using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SP18.PF.Core.Features.Users;
using SP18.PF.Web.Areas.Admin.Models.Users;
using SP18.PF.Web.Helpers;

namespace SP18.PF.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly DbContext dbContext;

        public AccountController(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectUser(returnUrl);
            }
            return View(new LoginViewModel
            {
                ReturnUrl = returnUrl
            });
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(GetViewModel(vm));
            }
            var user = await dbContext.Set<User>().SingleOrDefaultAsync(x => x.Email == vm.Email);
            if (user == null)
            {
                return DenyLogin(vm);
            }

            if (!CryptoHelpers.VerifyPassword(vm.Password, user.Password))
            {
                return DenyLogin(vm);
            }

            await CreateAuthenticationTicket(user, vm.RemeberMe);

            return RedirectUser(vm.ReturnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await HttpContext.SignOutAsync();
            }
            return RedirectToAction("Login", "Account", new { returnUrl = string.Empty });
        }

        private IActionResult RedirectUser(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private async Task CreateAuthenticationTicket(User user, bool persistant)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = persistant
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
        }

        private IActionResult DenyLogin(LoginViewModel vm)
        {
            ModelState.AddModelError("Email", "No such user or incorrect password");
            return View(GetViewModel(vm));
        }

        private static LoginViewModel GetViewModel(LoginViewModel vm)
        {
            return new LoginViewModel
            {
                ReturnUrl = vm?.ReturnUrl
            };
        }
    }
}
