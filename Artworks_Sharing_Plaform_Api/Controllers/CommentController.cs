using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [ApiController]
    [Route("CommentApi")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpGet("GetListCommentByPosts")]        
        public async Task<IActionResult> GetListCommentByPostsAsync(Guid postId)
        {
            try
            {
                var result = await _commentService.GetListCommentByPostsAsync(postId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("GetListCommentByArtworks")]        
        public async Task<IActionResult> GetListCommentByArtworksAsync(Guid artworkId)
        {
            try
            {
                var result = await _commentService.GetListCommentByArtworksAsync(artworkId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CreatePostComment")]        
        [Authorize]
        public async Task<IActionResult> CreatePostCommentAsync(CreatePostCommentResDto resDto)
        {
            try
            {
                var result = await _commentService.CreatePostCommentAsync(resDto);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CreateArtworkComment")]        
        [Authorize]
        public async Task<IActionResult> CreateArtworkCommentAsync([FromBody]CreateArtworkCommentResDto resDto)
        {
            try
            {
                var result = await _commentService.CreateArtworkCommentAsync(resDto);
                if (result)
                    return StatusCode(200, "Create comment success");
                return StatusCode(400, "Create comment fail");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("DeleteComment")]
        [Authorize]
        public async Task<IActionResult> DeleteCommentAsync(Guid commentId)
        {
            try
            {
                var result = await _commentService.DeleteArtworkCommentAsync(commentId);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
