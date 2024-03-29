using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [ApiController]
    [Route("typeofartworkapi")]
    public class TypeOfArtworkController : ControllerBase
    {
        private readonly ITypeOfArtworkService _typeOfArtworkService;

        public TypeOfArtworkController(ITypeOfArtworkService typeOfArtworkService)
        {
            _typeOfArtworkService = typeOfArtworkService;
        }

        [Authorize]
        [HttpPost("SaveTypeOfArtwork")]
        public async Task<IActionResult> SaveTypeOfArtwork([FromForm(Name = "Data")] IFormFile? file, [FromForm] TypeOfArtworkReqDto reqDto)
        {
            try
            { 

                if (ModelState.IsValid)
                {
                    if (await _typeOfArtworkService.CreateTypeOfArtworkAsync(file, reqDto))
                    {
                        return StatusCode(200, SucessfullyEnum.TypeOfArtwork.CREATE_TYPE_OF_ARTWORK_SUCCESS);
                    }
                    return StatusCode(500, ServerErrorEnum.SERVER_ERROR);
                } else
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
        [HttpGet("GetListRequestTypeOfArtwork")]
        public async Task<IActionResult> GetListRequestTypeOfArtwork()
        {
            try
            {

                if (ModelState.IsValid)
                {                    
                    return StatusCode(200, await _typeOfArtworkService.GetListRequestTypeOfArtwork());          
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

        [HttpGet("GetListTypeOfArtwork")]
        public async Task<IActionResult> GetListTypeOfArtwork()
        {
            try
            {                
                return StatusCode(200, await _typeOfArtworkService.GetListTypeOfArtworkAsync());
            }
            catch (Exception)
            {
                return StatusCode(500, ServerErrorEnum.SERVER_ERROR);
            }
        }

        [Authorize]
        [HttpGet("GetListTypeOfArtworkByRoleAdmin")]
        public async Task<IActionResult> GetListTypeOfArtworkAsyncByRoleAdmin()
        {
            try
            {
                return StatusCode(200, await _typeOfArtworkService.GetListTypeOfArtworkAsyncByRoleAdmin());
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
        [HttpPut("UpdateTypeOfArtwork")]
        public async Task<IActionResult> UpdateTypeOfArtworkAsync([FromBody] UpdateTypeOfArtworkReqDto reqDto)
        {
            try
            {
                return StatusCode(200, await _typeOfArtworkService.UpdateTypeOfArtworkAsync(reqDto));
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
                    case TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_NOT_FOUND:
                        statusCode = 408;
                        errorMessage = ex.Message;
                        break;
                    case TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_STATUS_NOT_FOUND:
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
        [HttpDelete("DeleteTypeOfArtwork")]
        public async Task<IActionResult> DeleteTypeOfArtworkAsync(Guid typeOfArtworkID)
        {
            try
            {
                return StatusCode(200, await _typeOfArtworkService.DeleteTypeOfArtworkAsync(typeOfArtworkID));
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
                    case TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_NOT_FOUND:
                        statusCode = 408;
                        errorMessage = ex.Message;
                        break;
                    case TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_STATUS_NOT_FOUND:
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
        [HttpPut("ActiveTypeOfArtwork")]
        public async Task<IActionResult> ActiveTypeOfArtworkAsync(Guid TypeOfArtworkID)
        {
            try
            {
                return StatusCode(200, await _typeOfArtworkService.ActiveTypeOfArtworkAsync(TypeOfArtworkID));
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
                    case TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_NOT_FOUND:
                        statusCode = 408;
                        errorMessage = ex.Message;
                        break;
                    case TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_STATUS_NOT_FOUND:
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
        [HttpPut("DeActiveTypeOfArtwork")]
        public async Task<IActionResult> DeActiveTypeOfArtworkAsync(Guid TypeOfArtworkID)
        {
            try
            {
                return StatusCode(200, await _typeOfArtworkService.DeActiveTypeOfArtworkAsync(TypeOfArtworkID));
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
                    case TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_NOT_FOUND:
                        statusCode = 408;
                        errorMessage = ex.Message;
                        break;
                    case TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_STATUS_NOT_FOUND:
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
        [HttpPut("ChangeStatusTypeOfArtworkByAdminAsync")]
        public async Task<IActionResult> ChangeStatusTypeOfArtworkByAdminAsync(ChangeStatusRequestDto requestDto)
        {
            try
            {
                return StatusCode(200, await _typeOfArtworkService.ChangeStatusTypeOfArtworkByAdminAsync(requestDto));
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
                    case TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_NOT_FOUND:
                        statusCode = 408;
                        errorMessage = ex.Message;
                        break;
                    case TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_STATUS_NOT_FOUND:
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
        [HttpGet("GetListTypeOfArtworkAsyncByRoleCreator")]
        public async Task<IActionResult> GetListTypeOfArtworkAsyncByRoleCreator()
        {
            try
            {
                return StatusCode(200, await _typeOfArtworkService.GetListTypeOfArtworkAsyncByRoleCreator());
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
                    case TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_NOT_FOUND:
                        statusCode = 408;
                        errorMessage = ex.Message;
                        break;
                    case TypeOfArtworkErrorEnum.TYPE_OF_ARTWORK_STATUS_NOT_FOUND:
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
        [HttpGet("GetListPendingTypeOfArtwork")]
        public async Task<IActionResult> GetListPendingTypeOfArtwork()
        {
            try
            {
                return StatusCode(200, await _typeOfArtworkService.GetListPendingTypeOfArtworkAsync());
            }
            catch (Exception ex)
            {
               return BadRequest(ex.Message);
            }
        }
    }
}
