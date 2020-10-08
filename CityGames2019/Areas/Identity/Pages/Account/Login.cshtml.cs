using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Identity.UI.Services;
using CityGames2019.Data;
using CityGames2019.Resources;
using CityGames2019.Models;

namespace CityGames2019.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<LoginModel> _loggerLogin;
        private readonly ILogger<RegisterModel> _loggerRegister;
        private readonly IEmailSender _emailSender;
        private LocService _sharedLocalizer;
        private ApplicationDbContext _context;

        public LoginModel(
            SignInManager<User> signInManager,
            ILogger<LoginModel> loggerLogin,
            UserManager<User> userManager,
            ILogger<RegisterModel> loggerRegister,
            IEmailSender emailSender,
            LocService sharedLocalizer,
            ApplicationDbContext context
            )
        {
            _signInManager = signInManager;
            _loggerLogin = loggerLogin;
            _context = context;
            _userManager = userManager;
            _loggerRegister = loggerRegister;
            _emailSender = emailSender;
            _sharedLocalizer = sharedLocalizer;
        }

        [BindProperty]
        public InputLoginModel InputLogin { get; set; }

        [BindProperty]
        public InputRegisterModel InputRegister { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputLoginModel
        {
            //[Required]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            //[Required]
            [DataType(DataType.Password)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [Display(Name = "RememberMe")]
            public bool RememberMe { get; set; }
        }

        public class InputRegisterModel
        {
            //[Required]
            [EmailAddress(ErrorMessage = "ValidError_Email")]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Display(Name = "Email")]
            public string Email { get; set; }

            //[Required]
            [StringLength(30, ErrorMessage = "ValidError_Username", MinimumLength = 3)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [Display(Name = "Username")]
            public string Username { get; set; }

            //[Required]
            [StringLength(100, ErrorMessage = "ValidError_Password", MinimumLength = 6)]
            [DisplayFormat(ConvertEmptyStringToNull = false)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "RePassword")]
            [Compare("Password", ErrorMessage = "ValidError_RePassword")]
            public string ConfirmPassword { get; set; }

            [Display(Name = "Name")]
            public string Name { get; set; }

            [Display(Name = "Surname")]
            public string Surname { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string button, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            //var errors = ModelState.Values.SelectMany(v => v.Errors);

            if (ModelState.IsValid)
            {
                if (button.Equals("LogIn"))
                {
                    // This doesn't count login failures towards account lockout
                    // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                    var result = await _signInManager.PasswordSignInAsync(InputLogin.Username, InputLogin.Password, InputLogin.RememberMe, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {
                        var user = _context.Users.FirstOrDefault(us => us.UserName == InputLogin.Username);
                        returnUrl = Url.Content("~/User/Index/" + user.Id);
                        _loggerLogin.LogInformation("User logged in.");
                        return LocalRedirect(returnUrl);
                    }
                    if (result.RequiresTwoFactor)
                    {
                        return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = InputLogin.RememberMe });
                    }
                    if (result.IsLockedOut)
                    {
                        _loggerLogin.LogWarning("User account locked out.");
                        return RedirectToPage("./Lockout");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, _sharedLocalizer.GetLocalizedHtmlString("InvalidLoginAttempt"));
                        return Page();
                    }
                }
                else
                {
                    var user = new IdentityUser { UserName = InputRegister.Username, Email = InputRegister.Email };

                    User appUser = new User(user);
                    appUser.Name = InputRegister.Name;
                    appUser.Surname = InputRegister.Surname;

                    var result = await _userManager.CreateAsync(appUser, InputRegister.Password);

                    if (result.Succeeded)
                    {
                        _loggerRegister.LogInformation("User created a new account with password.");

                        var code = await _userManager.GenerateEmailConfirmationTokenAsync(appUser);
                        var callbackUrl = Url.Page(
                            "/Account/ConfirmEmail",
                            pageHandler: null,
                            values: new { appUser = appUser.Id, code = code },
                            protocol: Request.Scheme);

                        await _emailSender.SendEmailAsync(InputRegister.Email, "Confirm your email",
                            $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                        await _signInManager.SignInAsync(appUser, isPersistent: false);

                        return LocalRedirect(returnUrl);
                    }
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
