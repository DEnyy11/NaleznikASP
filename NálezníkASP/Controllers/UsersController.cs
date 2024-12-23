using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NálezníkASP.DTO;
using NálezníkASP.Models;

namespace NálezníkASP.Controllers {

    public class UsersController : Controller {
        private UserManager<AppUser> userManager;
        private IPasswordHasher<AppUser> passwordHasher;


        public UsersController(UserManager<AppUser> userManager, IPasswordHasher<AppUser> passwordHasher) {
            this.userManager = userManager;
            this.passwordHasher = passwordHasher;
        }

        public IActionResult Index() {
            return View(userManager.Users);
        }

        public IActionResult Create() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(UserDto newUser) {
            if (ModelState.IsValid) {
                AppUser appUser = new AppUser() {
                    Email = newUser.Email,
                    UserName = newUser.Name,
                };
                IdentityResult identityResult = await userManager.CreateAsync(appUser, newUser.Password);
                if (identityResult.Succeeded) {
                    return RedirectToAction("Index");
                }
                else {
                    foreach (IdentityError error in identityResult.Errors) {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
            return View(newUser);

        }

        public async Task<IActionResult> Edit(string id) {
            var appUser = await userManager.FindByIdAsync(id);
            if (appUser == null) {
                return View("NotFound");
            }
            else {
                return View(appUser);
            }
        }
        [HttpPost]
        public async Task<IActionResult> Edit(string Id, string Email, string password) {
            AppUser userToEdit = await userManager.FindByIdAsync(Id);
            if (userToEdit != null) {
                userToEdit.Email = Email;
                userToEdit.PasswordHash = passwordHasher.HashPassword(userToEdit, password);
                IdentityResult result = await userManager.UpdateAsync(userToEdit);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }
                else {
                    foreach (IdentityError error in result.Errors)
                        ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            else {
                ModelState.AddModelError("", "User not found.");
            }
            return View(userToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id) {
            AppUser appUser = await userManager.FindByIdAsync(id);
            if (appUser != null) {
                IdentityResult result = await userManager.DeleteAsync(appUser);
                if (result.Succeeded) {
                    return RedirectToAction("Index");
                }
                else {
                    foreach (IdentityError error in result.Errors) {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            else {
                ModelState.AddModelError("", "User not found");
            }
            return RedirectToAction("Index");
        }


    }


}

