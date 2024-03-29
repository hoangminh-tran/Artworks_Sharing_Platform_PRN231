using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    [Route("PreOrderApi")]
    [ApiController]
    public class PreOrderController : Controller
    {
        private readonly IPreOrderService _preOrderService;

        public PreOrderController(IPreOrderService preOrderService)
        {
            _preOrderService = preOrderService;
        }

        [HttpPost("CreatePreOrderByCustomer")]
        [Authorize]
        public async Task<ActionResult> CreatePreOrderByCustomer(Guid artworkId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _preOrderService.CreatePreOrderAsync(artworkId);
                    if (result)
                        return StatusCode(200, "Create pre order success");
                    return StatusCode(400, "Create pre order fail");
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

        [HttpGet("GetListPreOrderByCustomer")]
        [Authorize]
        public async Task<ActionResult> GetListPreOrderByCustomer()
        {
            try
            {
                return Ok(await _preOrderService.GetListPreOrderByCustomerIdAsync());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("DeletePreOrderById")]
        [Authorize]
        public async Task<ActionResult> DeletePreOrderById(Guid preOrderId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = await _preOrderService.DeletePreOrderAsync(preOrderId);
                    if (result)
                        return StatusCode(200, "Delete pre order success");
                    return StatusCode(400, "Delete pre order fail");
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
