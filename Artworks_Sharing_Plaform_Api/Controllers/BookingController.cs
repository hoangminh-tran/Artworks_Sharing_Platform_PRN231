using Artworks_Sharing_Plaform_Api.Model.Dto.ReqDto;
using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artworks_Sharing_Plaform_Api.Controllers
{    
    [Route("BookingApi")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpPost("CreateBooking")]
        [Authorize]        
        public async Task<IActionResult> CreateBooking([FromBody] CreateBookingArtworkReqDto bookingArtworkReqDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _bookingService.CreateBookingAsync(bookingArtworkReqDto);
                if (result)
                {
                    return Ok("Booking created successfully");
                }
                return BadRequest("Booking creation failed");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        
        [HttpPost("UploadArtworkByBookingId")]
        [Authorize]
        public async Task<IActionResult> UploadArtworkByBookingId([FromForm(Name = "Data")] IFormFile file, [FromForm] CreateUploadArtworkForBookingReqDto createUploadArtworkForBookingReqDto)
        {
            try
            {                
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _bookingService.UploadArtworkByBookingIdAsync(file, createUploadArtworkForBookingReqDto);
                if (result)
                {
                    return Ok("Artwork uploaded successfully");
                }
                return BadRequest("Artwork upload failed");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("UploadArtworkByRequestArtworkId")]
        [Authorize]
        public async Task<IActionResult> UploadArtworkByRequestArtworkAsync([FromForm(Name = "Data")] IFormFile file, [FromForm] CreateUploadArtworkForRequestBookingArtworkResDto resDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _bookingService.UploadArtworkByRequestArtworkAsync(file, resDto);
                if (result)
                {
                    return Ok("Artwork uploaded successfully");
                }
                return BadRequest("Artwork upload failed");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetListBookingByCustomer")]
        [Authorize]
        public async Task<IActionResult> GetListBookingByCustomer()
        {
            try
            {
                var result = await _bookingService.GetListBookingByCustomerIdAsync();
                if (result != null)
                {
                    return Ok(result);
                }
                return BadRequest("No booking found");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetBookingByBookingIdByCustomer")]
        [Authorize]
        public async Task<IActionResult> GetBookingByBookingIdByCustomer(Guid bookingId)
        {
            try
            {
                return Ok(await _bookingService.GetBookingByBookingIdByCustomerAsync(bookingId));
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("CreateRequestBookingArtworkByBookingId")]
        [Authorize]
        public async Task<IActionResult> CreateRequestBookingArtworkByBookingId([FromBody] CreateRequestBookingArtworkResDto createRequestBookingArtworkResDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _bookingService.CreateRequestBookingArtworkByBookingIdAsync(createRequestBookingArtworkResDto);
                if (result)
                {
                    return Ok("Request created successfully");
                }
                return BadRequest("Request creation failed");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("GetListBookingByCreatorId")]
        [Authorize]
        public async Task<IActionResult> GetListBookingByCreatorIdAsync()
        {
            try
            {
                return Ok(await _bookingService.GetListBookingByCreatorIdAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("GetBookingByBookingIdByCreator")]
        [Authorize]
        public async Task<IActionResult> GetBookingByBookingIdByCreatorAsync(Guid bookingId)
        {
            try
            {
                return Ok(await _bookingService.GetBookingByBookingIdByCreatorAsync(bookingId));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ChangeStatusBookingByCreator")]
        [Authorize]
        public async Task<IActionResult> ChangeStatusBookingByCreatorAsync([FromBody] ChangeStatusBookingByCreatorReqDto changeStatusBookingByCreatorReqDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _bookingService.ChangeStatusBookingByCreatorAsync(changeStatusBookingByCreatorReqDto);
                if (result)
                {
                    return Ok("Status changed successfully");
                }
                return BadRequest("Status change failed");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpGet("GetListBookingByAdmin")]
        [Authorize]
        public async Task<IActionResult> GetListBookingByAdminAsync()
        {
            try
            {
                return Ok(await _bookingService.GetListBookingByAdminAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("ChangeStatusRequestBookingArtworkByCreator")]
        [Authorize]
        public async Task<IActionResult> ChangeStatusRequestBookingArtworkByCreatorAsync([FromBody] ChangeStatusRequestByCreatorResDto resdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _bookingService.ChangeStatusRequestByCreatorAsync(resdto);
                if (result)
                {
                    return Ok("Status changed successfully");
                }
                return BadRequest("Status change failed");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("ChangeStatusRequestBookingArtworkByCustomer")]
        [Authorize]
        public async Task<IActionResult> ChangeStatusRequestBookingArtworkByCustomerAsync([FromBody] ChangeStatusRequestBookingByCustomerReqDto resdto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                var result = await _bookingService.ChangeStatusRequestBookingByCustomerAsync(resdto);
                if (result)
                {
                    return Ok("Status changed successfully");
                }
                return BadRequest("Status change failed");
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
