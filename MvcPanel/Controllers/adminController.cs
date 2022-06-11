using Microsoft.AspNetCore.Mvc;

namespace MvcPanel.Controllers
{
    public class adminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
