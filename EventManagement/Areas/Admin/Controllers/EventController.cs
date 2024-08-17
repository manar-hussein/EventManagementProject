using Microsoft.AspNetCore.Mvc;

namespace EventManagement.Areas.Services.Controllers
{
    [Area("Admin")]
    public class EventController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
