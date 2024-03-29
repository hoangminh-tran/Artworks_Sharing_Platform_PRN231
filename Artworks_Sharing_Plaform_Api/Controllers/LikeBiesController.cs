using Microsoft.AspNetCore.Mvc;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Enum;
using Microsoft.AspNetCore.Authorization;
using static Artworks_Sharing_Plaform_Api.Enum.SucessfullyEnum;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [Route("likebyapi")]    
    [ApiController]
    public class LikeBiesController : Controller
    {
        private readonly ILikeByService _likeByService;
        

        public LikeBiesController(ILikeByService likeByService)
        {
            _likeByService = likeByService;
        }

        [HttpPost("likeartwork")]
        [Authorize]
        public async Task<ActionResult<LikeBy>> LikeArtworkAsync(Guid artworkId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _likeByService.LikeArtWorkAsync(artworkId);
                    if (result)
                        return StatusCode(200, LikeBySucessEnum.LIKE_BY_ADD_SUCESS);
                    return StatusCode(400, LikeByErrorEnum.LIKE_BY_ADD_FAIL);
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

                    case ArtWorkErrorEnum.ARTWORK_NOT_FOUND:
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

        [HttpPost("likecomment")]
        [Authorize]
        public async Task<ActionResult<LikeBy>> LikeCommentAsync(LikeCommentDto likeCommentDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _likeByService.LikeCommentAsync(likeCommentDto.CommentId);
                    if (result)
                        return StatusCode(200, LikeBySucessEnum.LIKE_BY_ADD_SUCESS);
                    return StatusCode(400, LikeByErrorEnum.LIKE_BY_ADD_FAIL);
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

                    case CommentErrorEnum.COMMENT_NOT_FOUND:
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

        [HttpPost("likepost")]
        [Authorize]
        public async Task<ActionResult<LikeBy>> LikePostAsync(Guid postId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _likeByService.LikePostAsync(postId);
                    if (result)
                        return StatusCode(200, LikeBySucessEnum.LIKE_BY_ADD_SUCESS);
                    return StatusCode(400, LikeByErrorEnum.LIKE_BY_ADD_FAIL);
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

                    case PostErrorEnum.POST_NOT_FOUND:
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

        [HttpPost("unlikeartwork")]
        [Authorize]
        public async Task<ActionResult<LikeBy>> UnLikeArtworkAsync(Guid artworkId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _likeByService.UnlikeArtWorkAsync(artworkId));
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

                    case ArtWorkErrorEnum.ARTWORK_NOT_FOUND:
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

        [HttpPost("unlikepost")]
        [Authorize]
        public async Task<ActionResult<LikeBy>> UnLikePostAsync(Guid postId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _likeByService.UnlikePostAsync(postId));
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

                    case PostErrorEnum.POST_NOT_FOUND:
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
    }
}
