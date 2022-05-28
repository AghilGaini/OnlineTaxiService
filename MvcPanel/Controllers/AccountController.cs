using DatabaseDomain.DTOs.Account.Login;
using DatabaseDomain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MvcPanel.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public IActionResult Login(string ReturnUrl)
        {
            var loginDTO = new loginDTO()
            {
                ReturnUrl = ReturnUrl
            };
            return View(loginDTO);
        }

        [HttpPost]
        public async Task<IActionResult> Login(loginDTO model, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork._user.GetByUsername(model.Username);

                if (user == null || user.Password != model.Password)
                {
                    ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه میباشد");
                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Id.ToString())
                };

                var identity = new ClaimsIdentity(claims, "an");
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync("an", principal,
                    new AuthenticationProperties() { IsPersistent = model.RememberMe });

                if (model.ReturnUrl != null)
                    return LocalRedirect(model.ReturnUrl);

                return RedirectToAction("Index", "Home");

            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync("an");

            return RedirectToAction("Login");
        }
    }
}
