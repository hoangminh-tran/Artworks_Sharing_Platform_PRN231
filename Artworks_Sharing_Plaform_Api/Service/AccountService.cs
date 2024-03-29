using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Model.Dto.ResDto;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IConfiguration _configuration;
        private readonly IStatusRepository _statusRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IHelpperService _helperService;
        private readonly IStatusService _statusService;
        private readonly IEmailService _emailService;

        public AccountService(IAccountRepository accountRepository, IConfiguration configuration, IStatusRepository statusRepository, IRoleRepository roleRepository, IHelpperService helperService, IStatusService statusService, IEmailService emailService)
        {
            _accountRepository = accountRepository;
            _configuration = configuration;
            _statusRepository = statusRepository;
            _roleRepository = roleRepository;
            _helperService = helperService;
            _statusService = statusService;
            _emailService = emailService;
        }

        public async Task<string> LoginAsync(LoginReqDto acc)
        {
            try
            {
                var account = await _accountRepository.GetAccountByEmailAsync(acc.Email);
                if (account == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                }
                var statusActive = await _statusService.GetStatusByStatusName(StatusEnum.ACTIVE) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (account.StatusId != statusActive.Id) throw new Exception("Account not active");
                else
                {
                    if (VerifyPasswordHash(acc.Password, Convert.FromBase64String(account.PasswordSalt), account.PasswordHash))
                    {
                        return CreateBearerTokenAccount(account);
                    }
                    else
                    {
                        throw new Exception(AccountErrorEnum.LOGIN_FAILED);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateAccountMemberAsync(CreateAccountReqDto acc)
        {
            try
            {
                var roleMember = await _roleRepository.GetRoleByNameAsync("MEMBER");
                return roleMember == null ? throw new Exception(ServerErrorEnum.SERVER_ERROR) : await CreateAccountMainAsync(acc, roleMember.Id, AccountStatusEnum.ACTIVE);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<bool> CreateAccountCreatorAsync(CreateAccountReqDto acc)
        {
            try
            {
                var roleMember = await _roleRepository.GetRoleByNameAsync("CREATOR");
                return roleMember == null ? throw new Exception(ServerErrorEnum.SERVER_ERROR) : await CreateAccountMainAsync(acc, roleMember.Id, AccountStatusEnum.PENDING);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateAccountAdminAsync(CreateAccountReqDto acc)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var roleAdmin = await _roleRepository.GetRoleByNameAsync("ADMIN") ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                // check if acc logged is admin
                if (accLoggedId.RoleId != roleAdmin.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                return await CreateAccountMainAsync(acc, roleAdmin.Id, AccountStatusEnum.ACTIVE);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ICollection<GetAccountResponseDto>?> GetListAccountAsync()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                var roleName = await _roleRepository.GetRoleByNameAsync("ADMIN") ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                // Check Whether the Account with Role ADMIN is existed 
                if (account.RoleId != roleName.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }

                var listAccount = await _accountRepository.GetListAccountAsync();

                var responseAccountList = listAccount?.Select(account => new GetAccountResponseDto
                {
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Email = account.Email,
                    PhoneNumber = account.PhoneNumber,
                    StatusName = account.Status?.StatusName ?? "Unknown",
                    RoleName = account.Role?.RoleName ?? "Unknown"
                }).ToList();
                return responseAccountList;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateProfileAccountAsync(UpdateProfileReqDto account)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }

                var accountExist = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                if (!String.IsNullOrEmpty(account.FirstName?.Trim()))
                {
                    accountExist.FirstName = account.FirstName;
                }
                if (!String.IsNullOrEmpty(account.LastName?.Trim()))
                {
                    accountExist.LastName = account.LastName;
                }
                if (!String.IsNullOrEmpty(account.PhoneNumber?.Trim()))
                {
                    accountExist.PhoneNumber = account.PhoneNumber;
                }
                return await _accountRepository.UpdateAccountAsync(accountExist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> GetRoleAccountLoggedInAsync()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                var role = await _roleRepository.GetRoleByRoleIDAsync(account.RoleId) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                return role.RoleName;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetCreatorResDto>> GetListCreatorAsync()
        {
            var roleCreator = await _roleRepository.GetRoleByNameAsync(RoleEnum.CREATOR) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
            var listCreator = await _accountRepository.GetAllAccountByRoleIdAsync(roleCreator.Id);
            List<GetCreatorResDto> listCreatorResDto = [];
            if (listCreator == null)
            {
                return listCreatorResDto;
            }
            foreach (var item in listCreator)
            {
                listCreatorResDto.Add(new GetCreatorResDto
                {
                    CreatorId = item.Id,
                    CreatorFirstName = item.FirstName,
                    CreatorLastName = item.LastName,
                });
            }
            return listCreatorResDto;
        }



        public async Task<GetLoggedAccountResDto> GetLoggedAccountAsync()
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                return new GetLoggedAccountResDto
                {
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Email = account.Email,
                    PhoneNumber = account.PhoneNumber,
                    Balance = account.Balance
                };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetAccountResponseDto>> GetListCreatorWithActiveStatusAsync()
        {
            try
            {
                var roleCreator = await _roleRepository.GetRoleByNameAsync(RoleEnum.CREATOR) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);

                var statusName = await _statusService.GetStatusByStatusName(StatusEnum.ACTIVE) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);

                var listAccount = await _accountRepository.GetAllAccountStatusByRoleIdAsync(roleCreator.Id, statusName.Id);

                List<GetAccountResponseDto> listAccountResponseDtos = [];
                if (listAccount == null)
                {
                    return listAccountResponseDtos;
                }
                foreach (var item in listAccount)
                {
                    listAccountResponseDtos.Add(new GetAccountResponseDto
                    {
                        id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        PhoneNumber = item.PhoneNumber,
                        StatusName = item.Status.StatusName,
                        RoleName = item.Role.RoleName
                    });
                }
                return listAccountResponseDtos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetAccountResponseDto>> GetListAccountByRoleAdminAsync(string roleName)
        {

            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                var roleAccount = await _roleRepository.GetRoleByNameAsync(roleName) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                var listAccount = await _accountRepository.GetAllAccountByRoleIdAsync(roleAccount.Id);
                List<GetAccountResponseDto> listAccountResponseDtos = [];
                if (listAccount == null)
                {
                    return listAccountResponseDtos;
                }
                foreach (var item in listAccount)
                {
                    listAccountResponseDtos.Add(new GetAccountResponseDto
                    {
                        id = item.Id,
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        Email = item.Email,
                        PhoneNumber = item.PhoneNumber,
                        StatusName = item.Status.StatusName,
                        RoleName = item.Role.RoleName
                    });
                }
                return listAccountResponseDtos;

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteAccountByRoleAdminAsync(Guid id, string roleName)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                if (id == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                }
                var roleMember = await _roleRepository.GetRoleByNameAsync(roleName) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                var accountExist = await _accountRepository.GetAccountByIdAsync(id);
                if (accountExist == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                }
                if (roleMember.Id != accountExist.RoleId)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                }
                var status = await _statusService.GetStatusByStatusName("DEACTIVE");
                accountExist.DeleteDateTime = DateTime.Now;
                accountExist.StatusId = status.Id;
                return await _accountRepository.UpdateAccountAsync(accountExist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ActiveAccountAsync(Guid id)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                if (id == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                }
                var accountExist = await _accountRepository.GetAccountByIdAsync(id);
                if (accountExist == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_MEMBER_NOT_FOUND);
                }
                var status = await _statusService.GetStatusByStatusName("ACTIVE");
                accountExist.DeleteDateTime = null;
                accountExist.StatusId = status.Id;
                return await _accountRepository.UpdateAccountAsync(accountExist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateAccountbyRoleAdminAsync(UpdateAccountReqDto account)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                if (account.Id == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                }
                var accountExist = await _accountRepository.GetAccountByIdAsync(account.Id);
                if (accountExist == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_MEMBER_NOT_FOUND);
                }

                if (!String.IsNullOrEmpty(account.FirstName?.Trim()))
                {
                    accountExist.FirstName = account.FirstName;
                }
                if (!String.IsNullOrEmpty(account.LastName?.Trim()))
                {
                    accountExist.LastName = account.LastName;
                }
                if (!String.IsNullOrEmpty(account.PhoneNumber?.Trim()))
                {
                    accountExist.PhoneNumber = account.PhoneNumber;
                }
                return await _accountRepository.UpdateAccountAsync(accountExist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> ChangeAccountStatusByRoleAdminAsync(ChangeStatusRequestDto requestDto)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                if (requestDto.Id == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                }
                var status = await _statusService.GetStatusByStatusName(requestDto.StatusName.ToUpper());
                if (status == null)
                {
                    throw new Exception(ServerErrorEnum.SERVER_ERROR);
                }
                var accountExist = await _accountRepository.GetAccountByIdAsync(requestDto.Id);
                if (accountExist == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_MEMBER_NOT_FOUND);
                }

                accountExist.DeleteDateTime = (requestDto.StatusName.ToUpper() == AccountStatusEnum.DEACTIVE) ? DateTime.Now : null;
                accountExist.StatusId = status.Id;

                return await _accountRepository.UpdateAccountAsync(accountExist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> ChangePasswordNotAuthenAsync(ChangePasswordNotAuthenDTO passowordDTO)
        {
            var account = await _accountRepository.GetAccountByEmailAsync(passowordDTO.Email);
            if (account == null)
            {
                throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
            }
            if (account.OtpCode == null || account.OtpCode != passowordDTO.OtpCode)
            {
                throw new Exception(account.OtpCode == null ? "OTP IS NOT FOUND" : "OTP IS NOT MATCH");
            }
            if (account.OtpCodeExpired < DateTime.Now)
            {
                throw new Exception("OTP IS Expired");
            }
            var passwordHash = CreatePassHashAndPassSalt(passowordDTO.NewPassword, out byte[] passwordSalt);
            account.PasswordHash = passwordHash;
            account.PasswordSalt = Convert.ToBase64String(passwordSalt);
            await _accountRepository.UpdateAccountAsync(account);
            return "Change Password Successfully";

        }
        public async Task<string> ChangePasswordAuthenAsync(ChangePassowordAuthenDTO passowordDTO)
        {

            var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
            if (passowordDTO.OldPasssword == null || !VerifyPasswordHash(passowordDTO.OldPasssword, Convert.FromBase64String(account.PasswordSalt), account.PasswordHash))
            {
                throw new Exception("PASSWORD IS NOT CORRECT");
            }
            var passwordHash = CreatePassHashAndPassSalt(passowordDTO.NewPassword, out byte[] passwordSalt);
            account.PasswordHash = passwordHash;
            account.PasswordSalt = Convert.ToBase64String(passwordSalt);
            await _accountRepository.UpdateAccountAsync(account);
            return "Change Password Successfully";
        }

        public async Task<string> GetOPTChangePasswordAsync(string Email)
        {
            var account = await _accountRepository.GetAccountByEmailAsync(Email);
            DateTime now = DateTime.Now;
            DateTime newExpirationDate = now.AddMinutes(5);
            if (account != null)
            {
                Random random = new Random();
                int randomNumber = random.Next(0, 9999);
                string OTPNumber = randomNumber.ToString("D4");
                var emailSubject = "Send OTP to Confirm Password Change";
                var emailBody = $@"<!DOCTYPE html>
                    <html lang='en'>
                    <head>
                        <meta charset='UTF-8'>
                        <meta name='viewport' content='width=device-width, initial-scale=1.0'>
                        <title>Congratulations on Your Account Upgrade</title>
                    </head>
                    <body>
                        <div style='font-family: Arial, sans-serif;'>
                            <h2>Confirm Password Change</h2>
                            <p>Please use the following OTP code to confirm your password change</p>
                            <p><strong>OTP Code: {OTPNumber}</strong></p>
                            <p>If you did not request this password change, please ignore this email.</p>
                            <img src='https://support.content.office.net/en-us/media/7dbd87dd-c244-4d78-8fda-4408a08582cc.jpg' alt='OTP Image' style='max-width: 100%;'>
                        </div>
                    </body>
                    </html>";

                await _emailService.SendEmailAsync(account.Email, emailSubject, emailBody);
                account.OtpCode = OTPNumber;
                account.OtpCodeCreated = now;
                account.OtpCodeExpired = newExpirationDate;
                await _accountRepository.UpdateAccountAsync(account);
            }
            return account == null ? "Cant Not Find Email User In Data" : "Send Temporary Passwrod In Mail";
        }
        #region
        private async Task<bool> CreateAccountMainAsync(CreateAccountReqDto acc, Guid roleId, string StatusName)
        {
            var account = await _accountRepository.GetAccountByEmailAsync(acc.Email);
            if (account != null)
            {
                throw new Exception(AccountErrorEnum.ACCOUNT_EXISTED);
            }
            else
            {
                var passHash = CreatePassHashAndPassSalt(acc.Password, out byte[] passwordSalt);
                var statusObj = await _statusRepository.GetStatusByNameAsync(StatusName) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                var newAcc = new Account
                {
                    FirstName = acc.FirstName,
                    LastName = acc.LastName,
                    Email = acc.Email,
                    PasswordHash = passHash,
                    PasswordSalt = Convert.ToBase64String(passwordSalt),
                    PhoneNumber = acc.PhoneNumber,
                    StatusId = statusObj.Id,
                    RoleId = roleId,
                    CreateDateTime = DateTime.Now
                };
                return await _accountRepository.CreateAccountAsync(newAcc);
            }
        }

        private static string CreatePassHashAndPassSalt(string pass, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            return System.Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass)));
        }

        private static bool VerifyPasswordHash(string pass, byte[] passwordSalt, string passwordHash)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = System.Convert.ToBase64String(hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(pass)));
            return computedHash.Equals(passwordHash);
        }

        private string CreateBearerTokenAccount(Account loginedAcc)
        {
            List<Claim> claims =
            [
                new Claim(ClaimTypes.Sid, loginedAcc.Id.ToString()),
            ];
            var securityKey = _configuration.GetSection("AppSettings:Token").Value ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return token == null ? throw new Exception(ServerErrorEnum.SERVER_ERROR) : tokenHandler.WriteToken(token);
        }


        public async Task<bool> AcceptRequestRegisterCreator(bool isAccept, Guid accountId)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);

                var account = await _accountRepository.GetAccountByIdAsync(accountId);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                if (account == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                }
                var accountExist = await _accountRepository.GetAccountByIdAsync(account.Id);
                if (accountExist == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                }

                if (isAccept)
                {
                    var status = await _statusRepository.GetStatusByNameAsync("ACTIVE");
                    accountExist.StatusId = status!.Id;
                    return await _accountRepository.UpdateAccountAsync(accountExist);
                }
                else
                {
                    var status = await _statusRepository.GetStatusByNameAsync("DEACTIVE");
                    accountExist.StatusId = status!.Id;
                    return await _accountRepository.UpdateAccountAsync(accountExist);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<GetRequestCreatorResDto>> GetListRequestCreatorAsync()
        {
            if (!_helperService.IsTokenValid())
            {
                throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
            }
            var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
            var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
            if (accLoggedId.RoleId != role.Id)
            {
                throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
            }

            var roleCheck = await _roleRepository.GetRoleByNameAsync(RoleEnum.CREATOR);

            if (roleCheck == null)
            {
                throw new Exception(RoleErrorEnum.ROLE_NOT_FOUND);
            }
            var listAccount = await _accountRepository.GetListAccountAsync();

            List<GetRequestCreatorResDto> listRequestCreatorResDto = [];
            if (listAccount == null)
            {
                return listRequestCreatorResDto;
            }

            var listCreator = listAccount.Where(c => c.RoleId.Equals(roleCheck.Id));
            var status = await _statusRepository.GetStatusByNameAsync("PENDING");

            foreach (var item in listCreator)
            {
                if (item.StatusId.Equals(status!.Id))
                    listRequestCreatorResDto.Add(new GetRequestCreatorResDto
                    {
                        CreatorId = item.Id,
                        CreatorFirstName = item.FirstName,
                        CreatorLastName = item.LastName,
                        Email = item.Email,
                        StatusName = status!.StatusName
                    });
            }
            return listRequestCreatorResDto;

        }

        public async Task<List<GetAccountResponseDto>> GetListAccountCreatorByRoleAdminAsync()
        {
            if (!_helperService.IsTokenValid())
            {
                throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
            }
            var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
            var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
            if (accLoggedId.RoleId != role.Id)
            {
                throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
            }

            var roleCreator = await _roleRepository.GetRoleByNameAsync(RoleEnum.CREATOR) ?? throw new Exception(RoleErrorEnum.ROLE_NOT_FOUND);
            var status = await _statusRepository.GetStatusByNameAsync("PENDING") ?? throw new Exception(ServerErrorEnum.STATUS_NOT_FOUND);

            var listAccount = await _accountRepository.GetAllAccountNotIncludeStatusByRoleIdAsync(roleCreator.Id, status.Id);

            List<GetAccountResponseDto> listRequestCreatorResDto = [];
            if (listAccount == null)
            {
                return listRequestCreatorResDto;
            }

            foreach (var account in listAccount)
            {
                listRequestCreatorResDto.Add(new GetAccountResponseDto
                {
                    id = account.Id,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Email = account.Email,
                    PhoneNumber = account.PhoneNumber,
                    StatusName = account.Status?.StatusName ?? "Unknown",
                    RoleName = account.Role?.RoleName ?? "Unknown"
                });
            }
            return listRequestCreatorResDto;
        }

        public async Task<List<GetAccountResponseDto>> GetListCreatorRequestByRoleAdminAsync()
        {
            if (!_helperService.IsTokenValid())
            {
                throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
            }
            var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
            var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
            if (accLoggedId.RoleId != role.Id)
            {
                throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
            }

            var roleCreator = await _roleRepository.GetRoleByNameAsync(RoleEnum.CREATOR) ?? throw new Exception(RoleErrorEnum.ROLE_NOT_FOUND);
            var status = await _statusRepository.GetStatusByNameAsync("PENDING") ?? throw new Exception(ServerErrorEnum.STATUS_NOT_FOUND);

            var listAccount = await _accountRepository.GetAllAccountStatusByRoleIdAsync(roleCreator.Id, status.Id);

            List<GetAccountResponseDto> listRequestCreatorResDto = [];
            if (listAccount == null)
            {
                return listRequestCreatorResDto;
            }

            listAccount = listAccount.OrderByDescending(acc => acc.CreateDateTime).ToList();

            foreach (var account in listAccount)
            {
                listRequestCreatorResDto.Add(new GetAccountResponseDto
                {
                    id = account.Id,
                    FirstName = account.FirstName,
                    LastName = account.LastName,
                    Email = account.Email,
                    PhoneNumber = account.PhoneNumber,
                    StatusName = account.Status?.StatusName ?? "Unknown",
                    RoleName = account.Role?.RoleName ?? "Unknown"
                });
            }
            return listRequestCreatorResDto;
        }

        public async Task<bool> DeleteCreatorRequestAccountByRoleAdminAsync(Guid id)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var accLoggedId = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                var role = await _roleRepository.GetRoleByNameAsync(RoleEnum.ADMIN) ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);
                if (accLoggedId.RoleId != role.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHORIZED);
                }
                if (id == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                }
                var accountExist = await _accountRepository.GetAccountByIdAsync(id);
                if (accountExist == null)
                {
                    throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                }
                return await _accountRepository.RemoveAccountAsync(accountExist);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetArtistByCustomerResDto> GetArtistByCustomerAsync(Guid artistId)
        {
            try
            {
                var artistOb = await _accountRepository.GetAccountByIdAsync(artistId) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                GetArtistByCustomerResDto artist = new GetArtistByCustomerResDto
                {
                    ArtistEmail = artistOb.Email,
                    ArtistFirstName = artistOb.FirstName,
                    ArtistLastName = artistOb.LastName
                };
                return artist;
            } catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
