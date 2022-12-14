using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using KnowledgeSpace.BackendServer.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;

namespace KnowledgeSpace.BackendServer.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {
            [Required (ErrorMessage = "Email không được để trống")]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required(ErrorMessage = "Mật khẩu không được để trống")]
            [StringLength(100, ErrorMessage = " {0} chứa ít nhất {2} ký tự và tối đa {1} ký tự.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Mật khẩu")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Xác thực mật khẩu")]
            [Compare("Password", ErrorMessage = "Mật khẩu và mật khẩu xác nhận không khớp.")]
            public string ConfirmPassword { get; set; }

            [Required (ErrorMessage = "Tên đăng nhập không được để trống")]
            [Display(Name = "Tên đăng nhập")]
            [StringLength(50, ErrorMessage = " {0} chứa ít nhất {2} ký tự và tối đa {1} ký tự.", MinimumLength = 3)]
            public string username { get; set; }

            [Required (ErrorMessage = "Họ không được để trống") ]
            [Display(Name = "Họ")]
            [StringLength(50, ErrorMessage = " {0} chứa ít nhất {2} ký tự và tối đa {1} ký tự.", MinimumLength = 3)]
            public string FirstName { get; set; }

            [Required (ErrorMessage = "Tên không được để trống") ]
            [Display(Name = "Tên")]
            [StringLength(50, ErrorMessage = " {0} chứa ít nhất {2} ký tự và tối đa {1} ký tự.", MinimumLength = 3)]
            public string LastName { get; set; }

            [Required (ErrorMessage = "Ngày sinh không được để trống") ]
            [Display(Name = "Ngày sinh")]
            public string Dob { get; set; }

            [Phone]
            [Required (ErrorMessage = "Số điện thoại không được để trống") ]
            [Display(Name = "Số điện thoại")]
            public string PhoneNumber { get; set; }



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
            //if(Input.LastName == null)
            //{
            //    ModelState.AddModelError("Tên không thể để trống")
            //}
            if (ModelState.IsValid)
            {
                var user = new User {
                    Id = Guid.NewGuid().ToString(),
                    Email = Input.Email,
                    Dob = DateTime.Parse(Input.Dob),
                    UserName = Input.username,
                    LastName = Input.LastName,
                    FirstName = Input.FirstName,
                    PhoneNumber = Input.PhoneNumber,
                    CreateDate = DateTime.Now
                };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");
                    await _userManager.AddToRoleAsync(user, "Member");
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = user.Id, code = code },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Xác nhận email của bạn",
                        $"Vui lòng xác thực tài khoản bằng cách vào đường dẫn <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>Chọn vào đây</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email });
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
