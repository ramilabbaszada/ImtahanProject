using ExamApplication.Business.Abstract;
using ExamApplication.Business.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExamApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserService userService;

        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View(new UserDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(UserDto dto, string? returnUrl = null)
        {
            ViewBag.ReturnUrl = returnUrl;

            var result = await userService.LoginAsync(dto);
            if (!result.Success || result.Data == null)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Giriş uğursuz oldu.");
                return View(dto);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()),
                new Claim(ClaimTypes.Name, result.Data.UserName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            if (!string.IsNullOrWhiteSpace(returnUrl) && Url.IsLocalUrl(returnUrl))
                return Redirect(returnUrl);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View(new UserDto());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(UserDto dto)
        {
            var result = await userService.RegisterAsync(dto);
            if (!result.Success || result.Data == null)
            {
                ModelState.AddModelError(string.Empty, result.Message ?? "Qeydiyyat uğursuz oldu.");
                return View(dto);
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, result.Data.Id.ToString()),
                new Claim(ClaimTypes.Name, result.Data.UserName)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}
