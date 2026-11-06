using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Heladeria.Web.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task Login()
        {
            await HttpContext.ChallengeAsync(GoogleDefaults.AuthenticationScheme,
                new AuthenticationProperties() { RedirectUri = Url.Action("GoogleResponse") });
        }

        public async Task<IActionResult> GoogleResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            if (result?.Principal != null)
            {
                var email = result.Principal.FindFirst(ClaimTypes.Email)?.Value;

                var claimsLocales = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, result.Principal.FindFirst(ClaimTypes.Name)?.Value ?? ""),
                    new Claim(ClaimTypes.Email, email ?? "")
                };

                if (email != null && (email.ToLower() == "tomas.espinosa2015@gmail.com"))
                {
                    claimsLocales.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else
                {
                    claimsLocales.Add(new Claim(ClaimTypes.Role, "Usuario"));
                }

                var identidadLocal = new ClaimsIdentity(claimsLocales, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identidadLocal));
            }

            return RedirectToAction("Index", "Sabor");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}