using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Artworks_Sharing_Plaform_Api.Enum.SucessfullyEnum;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [Route("accountapi")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync(LoginReqDto acc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _accountService.LoginAsync(acc));
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case AccountErrorEnum.ACCOUNT_NOT_FOUND:
                        statusCode = 404;
                        errorMessage = ex.Message;
                        break;
                    case AccountErrorEnum.LOGIN_FAILED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [HttpPost("registermember")]
        public async Task<IActionResult> RegisterAsync(CreateAccountReqDto acc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _accountService.CreateAccountMemberAsync(acc))
                    {
                        return StatusCode(200, "Create account successfully");
                    }
                    else
                    {
                        return StatusCode(400, "Create account failed");
                    }
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case AccountErrorEnum.ACCOUNT_EXISTED:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    default:

                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [HttpPost("registercreator")]
        public async Task<IActionResult> RegisterCreatorAsync(CreateAccountReqDto acc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _accountService.CreateAccountCreatorAsync(acc))
                    {
                        return StatusCode(200, "Create account successfully");
                    }
                    else
                    {
                        return StatusCode(400, "Create account failed");
                    }
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case AccountErrorEnum.ACCOUNT_EXISTED:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    default:

                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpPost("registeradmin")]
        public async Task<IActionResult> RegisterAdminAsync(CreateAccountReqDto acc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _accountService.CreateAccountAdminAsync(acc))
                    {
                        return StatusCode(200, "Create account successfully");
                    }
                    else
                    {
                        return StatusCode(400, "Create account failed");
                    }
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case AccountErrorEnum.ACCOUNT_EXISTED:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpGet("getListAccountAsync")]
        public async Task<IActionResult> GetListAccountAsync()
        {
            try
            {
                var listAccount = await _accountService.GetListAccountAsync();
                if (listAccount != null)
                {
                    return StatusCode(200, listAccount);
                }
                else return StatusCode(200, "List Account is Empty");
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case AccountErrorEnum.ACCOUNT_EXISTED:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }


        [Authorize]
        [HttpGet("getListRequestCreatorAsync")]
        public async Task<IActionResult> GetListRequestCreatorAsync()
        {
            try
            {
                var listRequestCreators = await _accountService.GetListRequestCreatorAsync();
                if (listRequestCreators != null)
                {
                    return StatusCode(200, listRequestCreators);
                }
                else return StatusCode(200, "List Request is Empty");
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case RoleErrorEnum.ROLE_NOT_FOUND:
                        statusCode = 404;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }


        [Authorize]
        [HttpGet("GetRoleAccountLoggedIn")]
        public async Task<IActionResult> GetRoleAccountLoggedIn()
        {
            try
            {
                var role = await _accountService.GetRoleAccountLoggedInAsync();
                if (role != null)
                {
                    return StatusCode(200, role);
                }
                else return StatusCode(200, "Role is Empty");
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpPost("updateaccount")]
        public async Task<IActionResult> UpdateProfileAsync(UpdateProfileReqDto acc)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.UpdateProfileAccountAsync(acc);
                    if (result)
                        return StatusCode(200, AccountSuccessEnum.ACCOUNT_UPDATE_SUCCESS);
                    return StatusCode(400, AccountErrorEnum.ACCOUNT_UPDATE_FAILED);
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case AccountErrorEnum.ACCOUNT_NOT_FOUND:
                        statusCode = 404;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpPut("UpdateAccountbyRoleAdminAsync")]
        public async Task<IActionResult> UpdateAccountbyRoleAdminAsync(UpdateAccountReqDto account)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _accountService.UpdateAccountbyRoleAdminAsync(account);
                    if (result)
                        return StatusCode(200, AccountSuccessEnum.ACCOUNT_UPDATE_SUCCESS);
                    return StatusCode(400, AccountErrorEnum.ACCOUNT_UPDATE_FAILED);
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case AccountErrorEnum.ACCOUNT_NOT_FOUND:
                        statusCode = 404;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [HttpGet("GetListCreator")]
        public async Task<IActionResult> GetListCreatorAsync()
        {
            try
            {
                return StatusCode(200, await _accountService.GetListCreatorAsync());
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpGet("GetLoggedAccount")]
        public async Task<IActionResult> GetLoggedAccountAsync()
        {
            try
            {
                return StatusCode(200, await _accountService.GetLoggedAccountAsync());
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case AccountErrorEnum.ACCOUNT_NOT_FOUND:
                        statusCode = 404;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpGet("GetListMemeberByRoleAdminAsync")]
        public async Task<IActionResult> GetListMemeberByRoleAdminAsync()
        {
            try
            {
                return StatusCode(200, await _accountService.GetListAccountByRoleAdminAsync(RoleEnum.MEMBER));
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpGet("GetListCreatorByRoleAdminAsync")]
        public async Task<IActionResult> GetListCreatorByRoleAdminAsync()
        {
            try
            {
                return StatusCode(200, await _accountService.GetListAccountByRoleAdminAsync(RoleEnum.CREATOR));
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpDelete("DeleteMemeberAccountByRoleAdminAsync")]
        public async Task<IActionResult> DeleteMemeberAccountByRoleAdminAsync(Guid id)
        {
            try
            {
                return StatusCode(200, await _accountService.DeleteAccountByRoleAdminAsync(id, RoleEnum.MEMBER));
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    case AccountErrorEnum.ACCOUNT_NOT_FOUND:
                        statusCode = 408;
                        errorMessage = ex.Message;
                        break;
                    case AccountErrorEnum.ACCOUNT_MEMBER_NOT_FOUND:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpDelete("DeleteCreatorAccountByRoleAdminAsync")]
        public async Task<IActionResult> DeleteCreatorAccountByRoleAdminAsync(Guid id)
        {
            try
            {
                return StatusCode(200, await _accountService.DeleteAccountByRoleAdminAsync(id, RoleEnum.CREATOR));
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    case AccountErrorEnum.ACCOUNT_NOT_FOUND:
                        statusCode = 408;
                        errorMessage = ex.Message;
                        break;
                    case AccountErrorEnum.ACCOUNT_CREATOR_NOT_FOUND:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpPut("ActiveAccountAsync")]
        public async Task<IActionResult> ActiveAccountAsync(Guid id)
        {
            try
            {
                return StatusCode(200, await _accountService.ActiveAccountAsync(id));
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    case AccountErrorEnum.ACCOUNT_NOT_FOUND:
                        statusCode = 408;
                        errorMessage = ex.Message;
                        break;
                    case AccountErrorEnum.ACCOUNT_CREATOR_NOT_FOUND:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpPut("AcceptRequestRegisterCreator")]
        public async Task<IActionResult> AcceptRequestRegisterCreator(bool isAccept, Guid accountId)
        {
            try
            {
                return StatusCode(200, await _accountService.AcceptRequestRegisterCreator(isAccept, accountId));
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    case AccountErrorEnum.ACCOUNT_NOT_FOUND:
                        statusCode = 408;
                        errorMessage = ex.Message;
                        break;
                    case AccountErrorEnum.ACCOUNT_CREATOR_NOT_FOUND:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [HttpPost("changePasswordNotAuthentication")]
        public async Task<IActionResult> ChangePasswordNotAuthen(ChangePasswordNotAuthenDTO passowordDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return StatusCode(200, await _accountService.ChangePasswordNotAuthenAsync(passowordDTO));
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case AccountErrorEnum.ACCOUNT_NOT_FOUND:
                        statusCode = 408;
                        errorMessage = ex.Message;
                        break;
                    case "OTP IS NOT FOUND":
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    case "OTP IS NOT MATCH":
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    case "OTP IS Expired":
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }
        [Authorize]
        [HttpPost("changePasswordAuthentication")]
        public async Task<IActionResult> ChangePassword(ChangePassowordAuthenDTO passowordDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            try
            {
                return StatusCode(200, await _accountService.ChangePasswordAuthenAsync(passowordDTO));
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case AccountErrorEnum.ACCOUNT_NOT_FOUND:
                        statusCode = 408;
                        errorMessage = ex.Message;
                        break;
                    case "PASSWORD IS NOT CORRECT":
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpPut("ChangeAccountStatusByRoleAdminAsync")]
        public async Task<IActionResult> ChangeAccountStatusByRoleAdminAsync(ChangeStatusRequestDto requestDto)
        {
            try
            {
                return StatusCode(200, await _accountService.ChangeAccountStatusByRoleAdminAsync(requestDto));
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    case AccountErrorEnum.ACCOUNT_NOT_FOUND:
                        statusCode = 408;
                        errorMessage = ex.Message;
                        break;
                    case AccountErrorEnum.ACCOUNT_CREATOR_NOT_FOUND:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [HttpPost("{email}/forgot_password")]
        public async Task<IActionResult> ForGotPassword(string email)
        {
            return Ok(await _accountService.GetOPTChangePasswordAsync(email));
        }

        [HttpGet("GetListCreatorWithActiveStatusAsync")]
        public async Task<IActionResult> GetListCreatorWithActiveStatusAsync()
        {
            try
            {
                var listAccount = await _accountService.GetListCreatorWithActiveStatusAsync();
                if (listAccount != null)
                {
                    return StatusCode(200, listAccount);
                }
                else return StatusCode(200, "List Account is Empty");
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case AccountErrorEnum.ACCOUNT_EXISTED:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [HttpGet("GetArtistByCustomer")]
        public async Task<IActionResult> GetArtistByCustomerAsync(Guid artistId)
        {
            try
            {
                return StatusCode(200, await _accountService.GetArtistByCustomerAsync(artistId));
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case AccountErrorEnum.ACCOUNT_NOT_FOUND:
                        statusCode = 404;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpGet("GetListAccountCreatorByRoleAdminAsync")]
        public async Task<IActionResult> GetListAccountCreatorByRoleAdminAsync()
        {
            try
            {
                return StatusCode(200, await _accountService.GetListAccountCreatorByRoleAdminAsync());
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpGet("GetListCreatorRequestByRoleAdminAsync")]
        public async Task<IActionResult> GetListCreatorRequestByRoleAdminAsync()
        {
            try
            {
                return StatusCode(200, await _accountService.GetListCreatorRequestByRoleAdminAsync());
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpDelete("DeleteCreatorRequestAccountByRoleAdminAsync")]
        public async Task<IActionResult> DeleteCreatorRequestAccountByRoleAdminAsync(Guid id)
        {
            try
            {
                return StatusCode(200, await _accountService.DeleteCreatorRequestAccountByRoleAdminAsync(id));
            }
            catch (Exception ex)
            {
                int statusCode;
                string errorMessage;
                switch (ex.Message)
                {
                    case ServerErrorEnum.NOT_AUTHENTICATED:
                        statusCode = 401;
                        errorMessage = ex.Message;
                        break;
                    case ServerErrorEnum.NOT_AUTHORIZED:
                        statusCode = 403;
                        errorMessage = ex.Message;
                        break;
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }
    }
}
