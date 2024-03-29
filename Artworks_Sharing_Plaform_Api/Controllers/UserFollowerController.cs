using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Service;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [Route("userfollowerapi")]
    [ApiController]
    public class UserFollowerController : Controller
    {        
        private readonly IUserFollowerService _userFollowerService;

        public UserFollowerController(IUserFollowerService userFollowerService)
        {            
            _userFollowerService = userFollowerService;
        }

        [Authorize]
        [HttpPost("follow")]
        public async Task<IActionResult> FollowAsync(FollowDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _userFollowerService.FollowUserAsync(dto))
                    {
                        return StatusCode(200, "Follow successfully");
                    }
                    else
                    {
                        return StatusCode(400, "Follow failed");
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
                    case UserFollowerErrorEnum.REQUEST_USER_NOT_FOUND:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    case RequestArtworkErrorEnum.CREATOR_USER_NOT_FOUND:
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
        [HttpDelete("unfollow")]
        public async Task<IActionResult> UnfollowAsync(FollowDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _userFollowerService.UnfollowUserAsync(dto))
                    {
                        return StatusCode(200, "Unfollow successfully");
                    }
                    else
                    {
                        return StatusCode(400, "Unfollow failed");
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
                    case UserFollowerErrorEnum.REQUEST_USER_NOT_FOUND:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    case RequestArtworkErrorEnum.CREATOR_USER_NOT_FOUND:
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
    }
}
