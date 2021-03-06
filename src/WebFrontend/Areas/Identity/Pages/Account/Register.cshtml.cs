﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using Microsoft.Graph;
using WebFrontend.Model;
using WebFrontend.Services.Captcha.hCaptcha;
using Microsoft.Extensions.Options;

namespace WebFrontend.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        public readonly AuthOptions hCaptcha;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly RoleManager<ApplicationRole> _roleManager;

        private class hCaptchaResponse
        {
            public bool success { get; set; }
        }

        private class hCaptchaRequest
        {
            public string response { get; set; }
            public string secret { get; set; }
        }

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender,
            IOptions<AuthOptions> hCaptcha,
            IHttpContextAccessor httpContext,
            IHttpClientFactory httpClientFactory,
            RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roleManager = roleManager;
            this.hCaptcha = hCaptcha.Value;
            _httpContext = httpContext;
            _httpClientFactory = httpClientFactory;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public async Task CreateRole()
        {
            bool x = await _roleManager.RoleExistsAsync("User");
            if (!x)
            {
                var role = new ApplicationRole
                {
                    Name = "User"
                };
                await _roleManager.CreateAsync(role);
            }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                await CreateRole();
                string hCaptchaResponse = HttpContext.Request.Form["h-captcha-response"];

                if (hCaptchaResponse is null)
                {
                    return BadRequest();
                }

                using(var client = _httpClientFactory.CreateClient())
                {
                    var content = new Dictionary<string, string>
                    {
                        {"response", hCaptchaResponse},
                        {"secret", hCaptcha.SecretKey}
                    };

                    var response = await (
                        await client.PostAsync(
                            QueryHelpers.AddQueryString("https://hcaptcha.com/siteverify", content),
                            null
                        )).Content
                        .ReadAsStringAsync();

                    _logger.LogInformation(response);

                    if (!JsonSerializer.Deserialize<hCaptchaResponse>(response).success)
                    {
                        ModelState.AddModelError("Captcha failed", "User captcha failed");
                        return Page();
                    }
                }
                // var request = new HttpRequestMessage(HttpMethod.Post, "https://hcaptcha.com/siteverify");
                // var content = new StringContent(JsonSerializer.Serialize(new hCaptchaRequest
                // {
                //     response = hCaptchaResponse,
                //     secret = hCaptcha.SecretKey
                // }), Encoding.UTF8, "application/json");

                var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    var roleresult = await _userManager.AddToRoleAsync(user, "User");
                    if (roleresult.Succeeded)
                    {
                        _logger.LogInformation("Adding user to user role");
                    }
                    _logger.LogInformation("User created a new account with password.");

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
