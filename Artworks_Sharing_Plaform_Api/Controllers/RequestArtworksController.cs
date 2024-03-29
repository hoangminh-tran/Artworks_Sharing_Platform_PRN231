using Microsoft.AspNetCore.Mvc;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using static Artworks_Sharing_Plaform_Api.Enum.SucessfullyEnum;
using Artworks_Sharing_Plaform_Api.Enum;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [Route("api/requestartworkapi")]
    [ApiController]
    public class RequestArtworksController : ControllerBase
    {
        private readonly IRequestArtworkService _artworkService;

        public RequestArtworksController(IRequestArtworkService artworkService)
        {
            _artworkService = artworkService;
        }

        // POST: api/RequestArtworks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        [Route("change-status-requestartwork")]
        public async Task<ActionResult<RequestArtwork>> ChangeStatusOfRequestArtwork(bool isAccepted, Guid requestArtworkId)
        {
            try
            {
                var requestArtwork = await _artworkService.GetRequestArtworkByRequestArtworkId(requestArtworkId);
                if (requestArtwork != null)
                {
                    var result = await _artworkService.AcceptOrRejectRequestArtwork(isAccepted, requestArtworkId);
                    if (!String.IsNullOrEmpty(result.Trim()))
                    {
                        return StatusCode(200, RequestArtworkSuccessEnum.UPDATE_REQUEST_ARTWORK_STATUS_SUCCESS + $" : {result}");
                    }
                }
                return StatusCode(400, RequestArtworkErrorEnum.UPDATE_REQUEST_ARTWORK_STATUS_FAIL);
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
