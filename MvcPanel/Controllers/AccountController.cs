using Microsoft.AspNetCore.Mvc;

namespace MvcPanel.Controllers
{
    public class AccountController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
