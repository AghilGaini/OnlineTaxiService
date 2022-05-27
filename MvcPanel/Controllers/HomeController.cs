using DatabaseDomain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MvcPanel.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public ActionResult Index()
        {
            ViewBag.Success = "test";
            return View();
        }

        [HttpPost]
        public ActionResult Delete(long id)
        {
            return Content("id is : " + id);
        }
    }
}
