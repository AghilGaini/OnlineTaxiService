using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MvcPanel.Controllers
{
    [Authorize]
    public class PassengerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CalculateFee(double distance)
        {
            double fee = 0;

            if (distance < 1)
            {
                fee = 10000;
            }
            else if (distance >= 1 && distance < 10)
            {
                fee = 20000;
            }
            else if (distance >= 10 && distance < 50)
            {
                fee = 50000;
            }
            else
                fee = 100000;

            return Content(fee.ToString());

        }
    }
}
