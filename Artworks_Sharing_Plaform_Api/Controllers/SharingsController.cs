using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Artworks_Sharing_Plaform_Api.AppDatabaseContext;
using Artworks_Sharing_Plaform_Api.Model;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Microsoft.AspNetCore.Authorization;
using Artworks_Sharing_Plaform_Api.Enum;
using Artworks_Sharing_Plaform_Api.Service;
using Microsoft.AspNetCore.Http.HttpResults;
using static Artworks_Sharing_Plaform_Api.Enum.SucessfullyEnum;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [Route("api/sharingapi")]
    [ApiController]
    public class SharingsController : ControllerBase
    {
        private readonly ISharingService _sharingService;

        public SharingsController(ISharingService sharingService)
        {
            _sharingService = sharingService;
        }

        // POST: api/Sharings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        [Route("create-sharing-post-artwork")]
        public async Task<ActionResult<Sharing>> CreateSharingPostArtwork(SharePostArtworkDto sharePostArtworkDto)
        {
            try
            {
                var sharing = await _sharingService.CreateSharingPostArtwork(sharePostArtworkDto);
                if (sharing)
                {
                    return StatusCode(200, SharingPostArtworkSuccessEnum.SHARING_POST_ARTWORK_SUCCESSFUL);
                }
                return StatusCode(400, SharingPostArtworkErrorEnum.SHARING_POST_ARTWORK_FAIL);
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
                        statusCode = 404;
                        errorMessage = ex.Message;
                        break;
                    case PostArtworkErrorEnum.POST_ARTWORK_NOT_FOUND:
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
