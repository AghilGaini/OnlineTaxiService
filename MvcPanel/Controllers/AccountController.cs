using DatabaseDomain.DTOs.Account.Login;
using DatabaseDomain.DTOs.Account.Register;
using DatabaseDomain.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static CoreServices.Enums;

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
        public async Task<IActionResult> Login(loginDTO model)
        {
            if (ModelState.IsValid)
            {
                var user = await _unitOfWork._user.GetByUsernameAndUserType(model.Username, model.UserType);

                if (user == null || user.Password != model.Password)
                {
                    ModelState.AddModelError("", "نام کاربری یا رمز عبور اشتباه میباشد");
                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username)
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                    new AuthenticationProperties() { IsPersistent = model.RememberMe });

                if (model.ReturnUrl != null)
                    return LocalRedirect(model.ReturnUrl);
                else
                {
                    switch (user.UserType)
                    {
                        case (int)UserType.Admin:
                            return RedirectToAction("index", "admin");
                        case (int)UserType.Driver:
                            return RedirectToAction("index", "driver");
                        case (int)UserType.Passenger:
                            return RedirectToAction("index", "passenger");
                        default:
                            return RedirectToAction("AccessDenied");
                    }
                }
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO model)
        {
            if (ModelState.IsValid)
            {
                if (await _unitOfWork._user.IsDuplicateByUsernameAndUserType(model.Username, model.UserType, 0))
                {
                    ModelState.AddModelError("", "نام کاربری تکراری میباشد");
                    return View(model);
                }

                if (await _unitOfWork._user.RegisterUserDTO(model))
                {
                    _unitOfWork.Commit();

                    var claims = new List<Claim>() { new Claim(ClaimTypes.Name, model.Username) };
                    var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal,
                        new AuthenticationProperties() { IsPersistent = model.RememberMe });

                    return RedirectToAction("index", "home");
                }
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

    }
}
