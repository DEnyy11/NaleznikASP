using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NálezníkASP.Services;
using NálezníkASP.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NálezníkASP.Models;
using System.Globalization;

namespace NálezníkASP.Controllers {
    public class FindingController : Controller {
        private FindingService findingService;
		private UserManager<AppUser> userManager;
        CultureInfo culture = new CultureInfo("en-US");

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
            if (!ModelState.IsValid) {
                if (sourceView == "Coin") {
                    return View("NewCoin");
                }
            }
            bool IsCoin = false;
            if (sourceView == "Coin") {
                IsCoin = true;
            }
            var user = await userManager.GetUserAsync(HttpContext.User);
            await findingService.SaveFindingAsync(finding, user, IsCoin);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult NewCoin() {
            return View();
        }

        [HttpGet]
        public IActionResult NewArtifact() {
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> AllFidingsAsync(string filterType, string filterName) {
			var user = await userManager.GetUserAsync(HttpContext.User);
			if (string.IsNullOrEmpty(filterName) && (string.IsNullOrEmpty (filterType))){
			    var allFindings = await findingService.GetAllFindings(user);
			    return View(allFindings);
            }
            else {
                var filteredFindings = findingService.GetFilteredFindigns(user, filterType, filterName);
                return View (filteredFindings);
            }
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
        [HttpGet]
        public async Task<IActionResult> MapOfFindingsAsync() {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var allFindings = await findingService.GetAllFindings(user);
            return View(allFindings);
        }

	}

}
