using Artworks_Sharing_Plaform_Api.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace Artworks_Sharing_Plaform_Api.Controllers
{
    public class StatusController : Controller
    {
        private readonly IStatusService _statusService;
        public StatusController(IStatusService statusService)
        {
            _statusService = statusService;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
