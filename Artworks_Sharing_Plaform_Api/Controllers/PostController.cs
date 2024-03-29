using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [Route("PostApi")]
    [ApiController]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost("CreatePostByCreator")]
        [Authorize]
        public async Task<ActionResult> CreatePostByCreator([FromBody] CreatePostReqDto post)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _postService.CreatePostAsync(post);
                    if (result)
                        return StatusCode(200, "Create post success");
                    return StatusCode(400, "Create post fail");
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetListPostArtwork")]
        public async Task<ActionResult> GetListPost()
        {
            try
            {
                return Ok(await _postService.GetListPostAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        
        [Authorize]
        [HttpGet("GetListPostByCustomerArtwork")]
        public async Task<ActionResult> GetListPostByCustomer()
        {
            try
            {
                return Ok(await _postService.GetListPostByCustomerAsync());
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
                    default:
                        statusCode = 500;
                        errorMessage = "Server error";
                        break;
                }
                return StatusCode(statusCode, errorMessage);
            }
        }        
        
        [HttpGet("GetListPostArtworkByCreaterId")]
        public async Task<ActionResult> GetListPostArtworkByCreatorNameAsync(Guid creatorId)
        {
            try
            {
                return Ok(await _postService.GetListPostByCreatorIdAsync(creatorId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetPostById")]
        public async Task<ActionResult> GetPostById(Guid postId)
        {
            try
            {
                return Ok(await _postService.GetPostByIdAsync(postId));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize]
        [HttpPut("UpdatePost")]
        public async Task<ActionResult> UpdatePost(PostRequestDto postRequest)
        {
            try
            {
                return Ok(await _postService.UpdatePostAsync(postRequest));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
