using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NálezníkASP.Services;
using NálezníkASP.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NálezníkASP.Models;

namespace NálezníkASP.Controllers {
    public class FindingController : Controller {
        private FindingService findingService;
		private UserManager<AppUser> userManager;

		public FindingController(FindingService findingService,UserManager<AppUser> userManager) {
            this.findingService = findingService;
            this.userManager = userManager;
        }

        [Authorize]
        [HttpGet]
		public IActionResult Index() {
			return View();
		}

		public async Task<IActionResult> SaveAsync(FindingDto finding) {
            var user = await userManager.GetUserAsync(HttpContext.User);
            await findingService.SaveFindingAsync(finding, user);
            return RedirectToAction("Index");
        }

        public IActionResult NewCoin() {
            return View();
        }
        public IActionResult NewArtifact() {
            return View();
        }
        [HttpGet]
		public async Task<IActionResult> AllFidingsAsync() {
			var user = await userManager.GetUserAsync(HttpContext.User);
			var allFindings = await findingService.GetAllFindings(user);
			return View(allFindings);
		}


	}

}
