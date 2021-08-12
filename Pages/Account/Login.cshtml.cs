using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using ClaimsHandler.Models;
using ClaimsHandler.DataContext;

namespace ClaimsHandler.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly ILogger<LoginModel> _logger;
        private readonly ClaimsHandler.DataContext.InterviewContext _context;

        public LoginModel(ILogger<LoginModel> logger, ClaimsHandler.DataContext.InterviewContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            [MaxLength(10, ErrorMessage = "maximum {1} characters allowed")]
            [DataType(DataType.Text)]
            public string UserName { get; set; }

            [Required]
            [MaxLength(20, ErrorMessage = "maximum {1} characters allowed")]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            // Clear the existing external cookie
            #region snippet2
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
            #endregion

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;

            if (ModelState.IsValid)
            {
                // Use Input.Email and Input.Password to authenticate the user
                // with your custom authentication logic.
                //
                // For demonstration purposes, the sample validates the user
                // on the email address maria.rodriguez@contoso.com with 
                // any password that passes model validation.

                var user = await AuthenticateUser(Input.UserName, Input.Password);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    return Page();
                }

                #region snippet1
                var claims = new List<System.Security.Claims.Claim>
                {
                    // use full namespace to avoid conflicting with the model claim
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Name as String, user.UserName as String),
                    new System.Security.Claims.Claim("DisplayName", user.DisplayName as String),
                    new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "User"),
                };

                var claimsIdentity = new System.Security.Claims.ClaimsIdentity(
                    claims, CookieAuthenticationDefaults.AuthenticationScheme);

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

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new System.Security.Claims.ClaimsPrincipal(claimsIdentity),
                    authProperties);
                #endregion

                _logger.LogInformation("User {UserName} logged in at {Time}.",
                    user.UserName, DateTime.UtcNow);

                return RedirectToPage("../Index");
            }

            // Something failed. Redisplay the form.
            return Page();
        }

        private async Task<User> AuthenticateUser(string username, string password)
        {

            User user = await _context.Users.FirstOrDefaultAsync(m => m.UserName == username && m.Password == password && m.Active == true);
            return user;
        }
    }
}
