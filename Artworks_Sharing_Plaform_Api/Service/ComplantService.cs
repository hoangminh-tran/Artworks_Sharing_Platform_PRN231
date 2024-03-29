using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Repository.Interface;
using Artworks_Sharing_Plaform_Api.Service.Interface;

namespace Artworks_Sharing_Plaform_Api.Service
{
    public class ComplantService : IComplantService
    {
        private readonly IComplantRepository _complantRepository;       
        private readonly IStatusRepository _statusRepository;
        private readonly IHelpperService _helperService;
        private readonly IAccountRepository _accountRepository ;
        private readonly IRoleRepository _roleRepository;

        public ComplantService(IComplantRepository complantRepository, IStatusRepository statusRepository, IHelpperService helperService, IAccountRepository accountRepository, IRoleRepository roleRepository)
        {
            _complantRepository = complantRepository;             
            _statusRepository = statusRepository;
            _helperService = helperService;
            _accountRepository = accountRepository;
            _roleRepository = roleRepository;
        }

        public async Task<bool> CreateArtworkComplaint(ComplaintArtworkRequestDto complaintRequest)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                Complant complant = new()
                {
                    AccountComplantId = account.Id,
                    ArtworkId = complaintRequest.ArtworkId,
                    ComplantContent = complaintRequest.ComplaintDescription,
                    ComplantType = complaintRequest.ComplainType
                };
                bool result = await _complantRepository.CreateComplantAsync(complant);
                return result;
            }           
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreateCommentComplaint(ComplaintCommentRequestDto complaintRequest)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);

                Complant complant = new()
                {
                    AccountComplantId = account.Id,
                    CommentId = complaintRequest.CommentId,
                    ComplantContent = complaintRequest.ComplaintDescription,
                    ComplantType = complaintRequest.ComplainType
                };
                bool result = await _complantRepository.CreateComplantAsync(complant);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> CreatePostComplaint(ComplaintPostRequestDto complaintRequest)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);

                Complant complant = new()
                {
                    AccountComplantId = account.Id,
                    PostId = complaintRequest.PostId,
                    ComplantContent = complaintRequest.ComplaintDescription,
                    ComplantType = complaintRequest.ComplainType
                };

                bool result = await _complantRepository.CreateComplantAsync(complant);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> UpdateComplaintAsync(ComplaintRequestDto request)
        {
            try
            {
                if (!_helperService.IsTokenValid())
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }
                var account = await _accountRepository.GetAccountByIdAsync(_helperService.GetAccIdFromLogged()) ?? throw new Exception(AccountErrorEnum.ACCOUNT_NOT_FOUND);
                var roleName = await _roleRepository.GetRoleByNameAsync("MODERATOR") ?? throw new Exception(ServerErrorEnum.SERVER_ERROR);

                // Check Whether the Account with Role MODERATOR is existed 
                if (account.RoleId != roleName.Id)
                {
                    throw new Exception(ServerErrorEnum.NOT_AUTHENTICATED);
                }

                // Check whether the Complant ID is existed or not
                var ComplantDto = await _complantRepository.GetComplantByComplantIDAsync(request.ComplantID) ?? throw new Exception(ComplantErrorNum.COMPLANT_NOT_FOUND);

                // Check whether the Complant Status is existed or not
                var StatusRequest = await _statusRepository.GetStatusByStatusIDAsync(request.StatusID) ?? throw new Exception(ComplantErrorNum.COMPLANT_STATUS_NOT_FOUND);
                ComplantDto.StatusId = request.StatusID;

                // Check whether the Complant Account is existed or not
                var ComplantAccount = await _accountRepository.GetAccountByIdAsync(ComplantDto.AccountComplantId) ?? throw new Exception(ComplantErrorNum.COMPLANT_ACCOUNT_NOT_FOUND);

                // Check whether the Manage Complant Issues Account is existed or not
                var manageIssusAccount = await _accountRepository.GetAccountByIdAsync(ComplantDto.ManageIssuseAccountId);
                return manageIssusAccount == null
                    ? throw new Exception(ComplantErrorNum.MANAGE_ISSUES_ACCOUNT_NOT_FOUND)
                    : await _complantRepository.UpdateComplaintAsync(ComplantDto);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
