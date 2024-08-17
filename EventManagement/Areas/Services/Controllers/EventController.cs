using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Areas.Services.Controllers
{
    [Area("Services")]
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
