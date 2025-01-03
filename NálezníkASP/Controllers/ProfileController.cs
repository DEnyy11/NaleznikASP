
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NálezníkASP.DTO;
using NálezníkASP.Models;
using NálezníkASP.Services;

namespace NálezníkASP.Controllers {
    [Authorize]
    public class ProfileController : Controller {
        private Microsoft.AspNetCore.Identity.UserManager<AppUser> userManager;
        private ProfileService profileService;

        public ProfileController(ProfileService profileService, UserManager<AppUser> userManager) {
            this.profileService = profileService;
            this.userManager = userManager;
        }
        public async Task<IActionResult> IndexAsync() {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var ProfileData = await profileService.GetProfileData(user);
            return View(ProfileData);
        }

        [HttpGet]
        public async Task<IActionResult> EditAsync() {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var ProfileData = await profileService.GetProfileData(user);
            return View(ProfileData);
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile profilePicture) {
            if (profilePicture != null) {
                var user = await userManager.GetUserAsync(HttpContext.User);
                await profileService.UpdateProfilePicture(user, profilePicture);
                return RedirectToAction("Index");
            }
                return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> SaveProfileChanges(ProfileDto profile) {
            var user = await userManager.GetUserAsync(HttpContext.User);
            await profileService.UpdateProfile(profile,user);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Route("Profile/ProfileReview/{Username}")]
        public async Task<IActionResult> ProfileReview(string Username) {
            var userProfile = await profileService.GetUserProfileReview(Username);
            
            return View(userProfile);
        }

    }
}
