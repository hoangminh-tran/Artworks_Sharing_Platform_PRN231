using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [ApiController]
    [Route("callback")]
    public class CallbackController : Controller
    {
        private readonly IPaymentService _paymentService;

        public CallbackController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] int? amount, [FromQuery] int? appid, [FromQuery] string? apptransid, [FromQuery] string? bankcode, [FromQuery] string? checksum, [FromQuery] int? discountamount, [FromQuery] int? pmcid, [FromQuery] int? status)
        {
            try
            {
                if (status == 1)
                {
                    if (await _paymentService.CallbackZaloPay(apptransid))
                    {
                        return Redirect("http://localhost:3000");
                    }
                    else
                    {
                        return BadRequest("Deposit failed");
                    }
                }
                else
                {
                    return BadRequest("Deposit failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
