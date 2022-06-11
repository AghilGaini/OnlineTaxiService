using Microsoft.AspNetCore.Mvc;

namespace MvcPanel.Controllers
{
    public class driverController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
