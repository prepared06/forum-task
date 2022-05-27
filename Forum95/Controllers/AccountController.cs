using Forum95.Models;
using Forum95.Data;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Forum95.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly Forum95Context _ctx;
        public AccountController(ILogger<AccountController> logger, Forum95Context ctx)
        {
            _logger = logger;
            _ctx = ctx;
        }

        [HttpGet]
        public IActionResult LogIn()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogIn(RegisterAccount model)
        {
            if (ModelState.IsValid)
            {
                User user =_ctx.Users.FirstOrDefault(g=>g.Email==model.Email && g.UserName==model.Name);
                if (user != null)
                {
                    await Authenticate(user);
                    return RedirectToAction("Index", "Home");
                }              
            }
            return View(model);
        }
        private async Task Authenticate(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }






        [HttpGet]
        public IActionResult SingUp()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SingUp(RegisterAccount model)
        {
            if (ModelState.IsValid)
            {
                User user = new User()
                {
                    UserName=model.Name,
                    Email=model.Email
                };
                _ctx.Users.Add(user);
                _ctx.SaveChanges();

                await Authenticate(user);
                return RedirectToAction("Index", "Home");
            }
            return View(model);
        }
    }
}
