using Microsoft.AspNetCore.Mvc;

namespace MvcPanel.Controllers
{
    public class PassengerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
