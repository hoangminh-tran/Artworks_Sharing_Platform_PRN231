using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Service;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static Artworks_Sharing_Plaform_Api.Enum.SucessfullyEnum;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [Route("complantapi")]
    [ApiController]
    public class ComplantController : Controller
    {
        private readonly IComplantService _complantService;
        private readonly ILogger _logger;
        public ComplantController(IComplantService complantService, ILogger<AccountController> logger)
        {
            _complantService = complantService;
            _logger = logger;
        }

        [Authorize]
        [HttpPut("updateStatusComplainAsync")]
        public async Task<IActionResult> UpdateStatusComplainAsync(ComplaintRequestDto request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _complantService.UpdateComplaintAsync(request))
                    {
                        return StatusCode(200, ComplantSuccessEnum.COMPLANT_UPDATE_SUCCESS);
                    }
                    else return StatusCode(400, ComplantErrorNum.COMPLANT_UPDATE_FAILED);
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
                    case ComplantErrorNum.MANAGE_ISSUES_ACCOUNT_NOT_FOUND:
                        statusCode = 402;
                        errorMessage = ex.Message;
                        break;
                    case ComplantErrorNum.COMPLANT_ACCOUNT_NOT_FOUND:
                        statusCode = 406;
                        errorMessage = ex.Message;
                        break;
                    case ComplantErrorNum.COMPLANT_STATUS_NOT_FOUND:
                        statusCode = 405;
                        errorMessage = ex.Message;
                        break;
                    case ComplantErrorNum.COMPLANT_NOT_FOUND:
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
                        _logger.LogInformation(ex.Message);
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpPost("createCommentComplainAsync")]
        public async Task<IActionResult> CreateCommentComplainAsync(ComplaintCommentRequestDto request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _complantService.CreateCommentComplaint(request))
                    {
                        return StatusCode(200, ComplantSuccessEnum.COMPLANT_CREATE_SUCCESS);
                    }
                    else return StatusCode(400, ComplantErrorNum.COMPLANT_CREATE_FAILED);
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
                    case ComplantErrorNum.MANAGE_ISSUES_ACCOUNT_NOT_FOUND:
                        statusCode = 402;
                        errorMessage = ex.Message;
                        break;
                    case ComplantErrorNum.COMPLANT_ACCOUNT_NOT_FOUND:
                        statusCode = 406;
                        errorMessage = ex.Message;
                        break;
                    case ComplantErrorNum.COMPLANT_STATUS_NOT_FOUND:
                        statusCode = 405;
                        errorMessage = ex.Message;
                        break;
                    case ComplantErrorNum.COMPLANT_NOT_FOUND:
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
                        _logger.LogInformation(ex.Message);
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpPost("createPostComplainAsync")]
        public async Task<IActionResult> CreatePostComplainAsync(ComplaintPostRequestDto request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _complantService.CreatePostComplaint(request))
                    {
                        return StatusCode(200, ComplantSuccessEnum.COMPLANT_CREATE_SUCCESS);
                    }
                    else return StatusCode(400, ComplantErrorNum.COMPLANT_CREATE_FAILED);
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
                    case ComplantErrorNum.MANAGE_ISSUES_ACCOUNT_NOT_FOUND:
                        statusCode = 402;
                        errorMessage = ex.Message;
                        break;
                    case ComplantErrorNum.COMPLANT_ACCOUNT_NOT_FOUND:
                        statusCode = 406;
                        errorMessage = ex.Message;
                        break;
                    case ComplantErrorNum.COMPLANT_STATUS_NOT_FOUND:
                        statusCode = 405;
                        errorMessage = ex.Message;
                        break;
                    case ComplantErrorNum.COMPLANT_NOT_FOUND:
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
                        _logger.LogInformation(ex.Message);
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }

        [Authorize]
        [HttpPost("createArtworkComplainAsync")]
        public async Task<IActionResult> CreateArtworkComplainAsync(ComplaintArtworkRequestDto request)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _complantService.CreateArtworkComplaint(request))
                    {
                        return StatusCode(200, ComplantSuccessEnum.COMPLANT_CREATE_SUCCESS);
                    }
                    else return StatusCode(400, ComplantErrorNum.COMPLANT_CREATE_FAILED);
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
                    case ComplantErrorNum.MANAGE_ISSUES_ACCOUNT_NOT_FOUND:
                        statusCode = 402;
                        errorMessage = ex.Message;
                        break;
                    case ComplantErrorNum.COMPLANT_ACCOUNT_NOT_FOUND:
                        statusCode = 406;
                        errorMessage = ex.Message;
                        break;
                    case ComplantErrorNum.COMPLANT_STATUS_NOT_FOUND:
                        statusCode = 405;
                        errorMessage = ex.Message;
                        break;
                    case ComplantErrorNum.COMPLANT_NOT_FOUND:
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
                        _logger.LogInformation(ex.Message);
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }
    }
}
