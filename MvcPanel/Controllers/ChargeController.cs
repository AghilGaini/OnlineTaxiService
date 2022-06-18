using DatabaseDomain.DTOs.Charge;
using DatabaseDomain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace MvcPanel.Controllers
{
    [Authorize]
    public class ChargeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ChargeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShaparakPaymnet(string id)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(id);
            var plainText = System.Text.Encoding.UTF8.GetString(base64EncodedBytes);

            var payObject = JsonSerializer.Deserialize<Dictionary<string, object>>(plainText);

            ViewBag.Username = payObject["Username"];
            ViewBag.Amount = payObject["Amount"];

            return View();
        }

        [HttpPost]
        public IActionResult ShaparakPaymnet(ShaparakPaymentDTO model)
        {
            if (ModelState.IsValid)
            {

            }

            return View(model);
        }

    }
}
