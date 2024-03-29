using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Service;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [ApiController]
    [Route("artworktypeapi")]
    public class ArtworkTypeController : Controller
    {
        private readonly IArtworkTypeService _artworkTypeService;

        public ArtworkTypeController(IArtworkTypeService artworkTypeService)
        {
            _artworkTypeService = artworkTypeService;
        }

        [Authorize]
        [HttpPost("createArtworkTypeByListTypeOfArtwork")]
        public async Task<IActionResult> CreateArtworkTypeByListTypeOfArtworkAsync(CreateAndUpdateListArtworkTypeReqDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _artworkTypeService.CreateArtworkTypeByListTypeOfArtworkAsync(dto))
                    {
                        return StatusCode(200, SucessfullyEnum.ArtworkTypeSuccessEnum.CREATE_ARTWORK_TYPE_SUCCESS);
                    }
                    else
                    {
                        return StatusCode(400, ArtworkTypeErrorNum.CREATE_ARTWORK_TYPE_FAIL);
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
                    case ArtworkTypeErrorNum.ARTWORK_NOT_FOUND:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    case ArtworkTypeErrorNum.TYPE_OF_ARTWORK_NOT_FOUND:
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
        [HttpPut("updateArtworkTypeByListTypeOfArtwork")]
        public async Task<IActionResult> UpdateArtworkTypeByListTypeOfArtworkAsync(CreateAndUpdateListArtworkTypeReqDto dto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (await _artworkTypeService.UpdateArtworkTypeByListTypeOfArtworkAsync(dto))
                    {
                        return StatusCode(200, SucessfullyEnum.ArtworkTypeSuccessEnum.UPDATE_ARTWORK_TYPE_SUCCESS);
                    }
                    else
                    {
                        return StatusCode(400, ArtworkTypeErrorNum.UPDATE_ARTWORK_TYPE_FAIL);
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
                    case ArtworkTypeErrorNum.ARTWORK_NOT_FOUND:
                        statusCode = 409;
                        errorMessage = ex.Message;
                        break;
                    case ArtworkTypeErrorNum.TYPE_OF_ARTWORK_NOT_FOUND:
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
    }
}
