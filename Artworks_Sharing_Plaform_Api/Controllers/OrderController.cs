using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [Route("OrderApi")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("BuyArtworkByCustomer")]
        [Authorize]
        public async Task<ActionResult> BuyArtworkByCustomerAsync(Guid PreOrderId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _orderService.BuyArtworkAsync(PreOrderId);
                    if (result)
                        return StatusCode(200, "Buy artwork success");
                    return StatusCode(400, "Buy artwork fail");
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

        [HttpGet("getOrderByAccount")]
        [Authorize]
        public async Task<ActionResult> GetOrderByAccountId(Guid accountId)
        {
            try
            {
                var result = await _orderService.GetListOrderByCustomerIdAsync(accountId);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("getAllOrder")]
        [Authorize]
        public async Task<ActionResult> GetAllOrder()
        {
            try
            {
                var result = await _orderService.GetAllOrder();
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("{orderId}/Order")]
        [Authorize]
        public async Task<ActionResult> GetOrderById(Guid orderId)
        {

            try
            {
                var result = await _orderService.GetOrderById(orderId);
                return StatusCode(200, result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
