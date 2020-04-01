using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HelloNetCore.Identities;
using HelloNetCore.Models.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace HelloNetCore.Controllers
{
    public class SecurityController : Controller
    {
        private UserManager<AppIdentityUser> _userManager;
        SignInManager<AppIdentityUser> _signInManager;

        public SecurityController(UserManager<AppIdentityUser> userManager,
            SignInManager<AppIdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(loginViewModel);
            }

            var user = await _userManager.FindByNameAsync(loginViewModel.Username);
            if (user != null)
            {
                if (!await _userManager.IsEmailConfirmedAsync(user))
                {
                ModelState.AddModelError(String.Empty, "Confirm Your Email Please");
                return View(loginViewModel);
                }
            }

            var result = await _signInManager.PasswordSignInAsync(loginViewModel.Username, loginViewModel.Password, false, false);

            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Student");
            }

            ModelState.AddModelError(String.Empty, "Login Failed");
            return View(loginViewModel);
        }

        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Student");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(registerViewModel);
            }

            var user = new AppIdentityUser
            {
                UserName = registerViewModel.Username,
                Email = registerViewModel.Email,
                Age = registerViewModel.Age
            };

            var result = await _userManager.CreateAsync(user, registerViewModel.Password);

            if (result.Succeeded)
            {
                var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                var callBackUrl = Url.Action("ConfirmEmail", "Security", new { userId = user.Id , token}, Request.Scheme);

                // Send Email

                return RedirectToAction("Index", "Student");
            }

            return View(registerViewModel);
        }
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                return RedirectToAction("Index", "Student");
            }
            var user = await _userManager.FindByIdAsync(userId);
            if (User == null)
            {
                throw new ApplicationException("Unable to find the user");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                return View("ConfirmEmail");
            }
            return RedirectToAction("Index", "Student");
        }
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            if (string.IsNullOrEmpty(email)){
                return View();
            }
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return View();
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            var callbackUrl = Url.Action("ResetPassword", "Security", new { userId = user.Id , token});

            //send callback Url with email

            return RedirectToAction("ForgotPasswordEmailSent");
        }

        public IActionResult ForgotPasswordEmailSent()
        {
            return View();
        }
        public IActionResult ResetPassword(string userId, string token)
        {
            if(userId==null || token == null)
            {
                throw new ApplicationException("User Id or Token must be supplied for password reset");
            }

            var model = new ResetPasswordViewModel { Token = token };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(resetPasswordViewModel);
            }

            var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
            if (user == null)
            {
                throw new ApplicationException("User Not Found");
            }

            var result = await _userManager.ResetPasswordAsync(user, resetPasswordViewModel.Token, resetPasswordViewModel.Password);

            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirm");
            }

            return View();
        }
        public IActionResult ResetPasswordConfirm()
        {
            return View();
        }

    }
}