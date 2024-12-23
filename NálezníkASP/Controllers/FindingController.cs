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

		public async Task<IActionResult> SaveAsync(FindingDto finding, string sourceView) {
            bool IsCoin = false;
            if (sourceView == "Coin") {
                IsCoin = true;
            }
            var user = await userManager.GetUserAsync(HttpContext.User);
            await findingService.SaveFindingAsync(finding, user, IsCoin);
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
        [HttpPost]
        public async Task<IActionResult> Delete(int id) {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var subjectToDelete = await findingService.GetByIdAsync(id, user);
            if (subjectToDelete == null) {
                return View("NotFound");
            }
            await findingService.DeleteAsync(id);
            return RedirectToAction("AllFidings");
        }
		[HttpGet]
		public async Task<IActionResult> EditAsync(int id) {
			var user = await userManager.GetUserAsync(HttpContext.User);
			var finding = await findingService.GetByIdAsync(id, user);
			if (finding == null) {
				return View("NotFound");
			}

			return View(finding);
		}
		[HttpPost]
		public async Task<IActionResult> EditAsync(int id, FindingDto finding, string sourceView) {
			
			var user = await userManager.GetUserAsync(HttpContext.User);
			await findingService.UpdateAsync(id, finding, user, finding.Coin);
			return RedirectToAction("AllFidings");
		}


	}

}
