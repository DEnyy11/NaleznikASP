using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NálezníkASP.DTO;
using NálezníkASP.Models;
using reCAPTCHA.AspNetCore;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace NálezníkASP.Controllers {
    public class AccountController : Controller {
        private Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager;
        private SignInManager<AppUser> signInManager;
     

        public AccountController(Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) {
            this.userManager = userManager;
            this.signInManager = signInManager;
      

        }
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string? returnUrl) {
            LoginDto loginDto = new LoginDto();
            if (string.IsNullOrEmpty(returnUrl)) {
                returnUrl = Url.Content("~/");
            }
            loginDto.ReturnUrl = returnUrl;
      
            return View(loginDto);
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto loginDto) {
            if (ModelState.IsValid) {
                
                AppUser appUser = await userManager.FindByNameAsync(loginDto.UserName);
                if (appUser != null) {
                    Microsoft.AspNetCore.Identity.SignInResult signInResult = await signInManager.PasswordSignInAsync(appUser, loginDto.Password, loginDto.RememberMe, false);
                    if (signInResult.Succeeded) {
                        return Redirect(loginDto.ReturnUrl ?? "/");
                    }
                }
                ModelState.AddModelError("","Unknown username or bad password");
            }
            return View(loginDto);
        }
     
        public async Task<IActionResult> Logout() {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied() {
            return View();
        }
        [HttpGet]
        public IActionResult Register() {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> Register(RegisterDto registerDto) {
            if (ModelState.IsValid) { 

                var emailConfirm = await userManager.FindByEmailAsync(registerDto.Email);
                if (emailConfirm != null) {
                    ModelState.AddModelError("", "There is already an account with this email");
                    return View(registerDto);
                }

                var user = new AppUser {UserName = registerDto.UserName, Email = registerDto.Email };
                var result = await userManager.CreateAsync(user, registerDto.Password);

                if (result.Succeeded) {
                    await userManager.AddToRoleAsync(user, "User");
                    await signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors) {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View ();
            
        }

    }
}


