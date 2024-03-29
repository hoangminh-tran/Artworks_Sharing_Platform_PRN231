using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [ApiController]
    [Route("artworkapi")]
    public class ArtworkController : ControllerBase
    {
        private readonly IArtworkService _artworkService;
        public ArtworkController(IArtworkService artworkService)
        {
            _artworkService = artworkService;
        }

        [Authorize]
        [HttpPost("uploadimage")]
        public async Task<IActionResult> UploadImageAsync([FromForm(Name = "Data")] IFormFile file, [FromForm] UploadArtworkReqDto reqDto)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    if (await _artworkService.UploadArtworkByCreatorAsync(file, reqDto))
                    {
                        return StatusCode(200, "Upload artwork successfully");
                    }
                    return StatusCode(200, await _artworkService.UploadArtworkByCreatorAsync(file, reqDto));
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet("GetArtworkByCreatorWithArtworkId")]
        public async Task<IActionResult> GetArtworkByCreatorWithArtworkIdAsync(Guid artworkId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _artworkService.GetArtworkByArtworkIdByCreatorAsync(artworkId));
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet("GetListArtworkByCreator")]
        public async Task<IActionResult> GetListArtworkByCreatorWithArtworkIdAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _artworkService.GetListArtworkByCreatorAsync());
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpPut("UpdateArtwork")]
        public async Task<IActionResult> UpdateArtworkAsync(UpdateArtworkDto updateArtworkDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _artworkService.UpdateArtwork(updateArtworkDto));
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet("GetArtworkByCustomerWithArtworkId")]
        public async Task<IActionResult> GetArtworkByCustomerWithArtworkIdAsync(Guid artworkId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _artworkService.GetArtworkByArtworkIdByCustomerAsync(artworkId));
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [Authorize]
        [HttpGet("GetListArtworkByCustomer")]
        public async Task<IActionResult> GetListArtworkByCustomerWithArtworkIdAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _artworkService.GetListArtworkByCustomerAsync());
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetArtworkById")]
        public async Task<IActionResult> GetArtworkByIdAsync(Guid artworkId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _artworkService.GetArtworkByArtworkIdByGuestAsync(artworkId));
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetListArtworkOwnByCustomerId")]
        [Authorize]
        public async Task<IActionResult> GetListArtworkOwnByCustomerIdAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _artworkService.GetListArtworkOwnByUserAsync());
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetListArtworkByTypeOfArtworkIdAsync")]
        public async Task<IActionResult> GetListArtworkByTypeOfArtworkIdAsync(Guid typeOfArtworkId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _artworkService.GetListArtworkByTypeOfArtworkIdAsync(typeOfArtworkId));
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

        [HttpGet("GetPublicArtwork")]
        public async Task<IActionResult> GetListArtworkAsync()
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _artworkService.GetListArtworkAsync());
                }
                else
                {
                    return StatusCode(400, ModelState);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("GetListArtworkByCreatorIdAsync")]
        public async Task<IActionResult> GetListArtworkByCreatorIdAsync(Guid creatorId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _artworkService.GetListArtworkByCreatorIdAsync(creatorId));
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

        [HttpGet("GetListArtworkByArtworkNameAsync")]
        public async Task<IActionResult> GetListArtworkByArtworkNameAsync(string? artworkName)
        {
            try
            {
                return StatusCode(200, await _artworkService.GetListArtworkByArtworkNameAsync(artworkName));
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

        [HttpPost("GetListArtworkByFilterListTypeOfArtworkAndArtist")]
        public async Task<IActionResult> GetListArtworkByFilterListTypeOfArtworkAndArtistAsync(FilterArtworkByListTypeAndArtistReqDto? res)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    return StatusCode(200, await _artworkService.GetListArtworkByFilterListTypeOfArtworkAndArtistAsync(res));
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

    }
}
