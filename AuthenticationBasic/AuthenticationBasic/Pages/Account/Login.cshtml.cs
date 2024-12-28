using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AuthenticationBasic.Pages.Account
{
    public class LoginModel : PageModel
    {
        //public async Task<IActionResult> OnGet(string returnUrl = null)
        //{

        //    var claims = new List<Claim>
        //    {
        //        new Claim(ClaimTypes.Name, "Bartosz"),
        //        new Claim("FullName", "Bartosz Rzemek"),
        //        new Claim(ClaimTypes.Role, "Admin")
        //    };

        //    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        //    var authProperties = new AuthenticationProperties
        //    {

        //    };

        //    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        //    return LocalRedirect(returnUrl);
            
        //}

        public async void OnGet()
        {
            //Authentication
            //success
            //pobierz z bazay danych urzytkownika o id i hasle i sprawdz czy sie zgadza.

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, "Bartosz"),
                new Claim("FullName", "Bartosz Rzemek"),
                new Claim(ClaimTypes.Role, "Admin")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                //AllowRefresh = <bool>,
                // Refreshing the authentication session should be allowed.

                //ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(10),
                // The time at which the authentication ticket expires. A 
                // value set here overrides the ExpireTimeSpan option of 
                // CookieAuthenticationOptions set with AddCookie.

                //IsPersistent = true,
                // Whether the authentication session is persisted across 
                // multiple requests. When used with cookies, controls
                // whether the cookie's lifetime is absolute (matching the
                // lifetime of the authentication ticket) or session-based.

                //IssuedUtc = <DateTimeOffset>,
                // The time at which the authentication ticket was issued.

                //RedirectUri = <string>
                // The full path or absolute URI to be used as an http 
                // redirect response value.
            };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

        }
    }
}
