using Microsoft.AspNetCore.Mvc;
using NálezníkASP.Services;
using NálezníkASP.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using NálezníkASP.Models;
using System.Globalization;

namespace NálezníkASP.Controllers {
	[Authorize]
	public class FindingController : Controller {
		private FindingService findingService;
		private UserManager<AppUser> userManager;
		CultureInfo culture = new CultureInfo("en-US");

		public FindingController(FindingService findingService, UserManager<AppUser> userManager) {
			this.findingService = findingService;
			this.userManager = userManager;
		}

		[Authorize]
		[HttpGet]
		public IActionResult Index() {
			return View();
		}

		public async Task<IActionResult> SaveAsync(FindingDto finding, string sourceView) {
			var user = await userManager.GetUserAsync(HttpContext.User);
			finding.Username = user.UserName;
			if (ModelState.IsValid) {
				bool IsCoin = false;
				if (sourceView == "Coin") {
					IsCoin = true;
				}
				await findingService.SaveFindingAsync(finding, user, IsCoin);
				return RedirectToAction("Index");
			}
			var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage).ToList();
			ViewData["ValidationErrors"] = errors;

			return sourceView == "coin" ? View("NewCoin") : View("NewArtifact");
		}
		[HttpGet]
		[Authorize]
		public IActionResult NewCoin() {
			return View();
		}

		[HttpGet]
		[Authorize]
		public IActionResult NewArtifact() {
			return View();
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> MyFindingsAsync(string filterType, string filterName) {
			var user = await userManager.GetUserAsync(HttpContext.User);
			if (string.IsNullOrEmpty(filterName) && (string.IsNullOrEmpty(filterType))) {
				var allFindings = await findingService.GetAllFindings(user);
				return View(allFindings);
			}
			else {
				var filteredFindings = findingService.GetFilteredFindigns(user, filterType, filterName);
				return View(filteredFindings);
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
			return RedirectToAction("MyFindings");
		}
		[HttpPost]
		public async Task<IActionResult> DeleteAdmin(int id) {
			await findingService.DeleteAsync(id);
			return RedirectToAction("AllFindings");
		}

		[HttpGet]
		[Authorize]
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
			return RedirectToAction("MyFindings");
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> MapOfFindingsAsync() {
			var user = await userManager.GetUserAsync(HttpContext.User);
			var allFindings = await findingService.GetAllFindings(user);
			return View(allFindings);
		}
		[HttpGet]
		public async Task<IActionResult> Detail(int id) {
			var finding = await findingService.GetByIdWithoutUserAsync(id);
			if (finding == null) {
				return View("NotFound");
			}
			return View(finding);
		}
		[HttpGet]
		[Authorize]
		public async Task<IActionResult> AllFindingsAsync(string filterName, string filterNameUser, string filterType) {
			if (string.IsNullOrEmpty(filterName) && (string.IsNullOrEmpty(filterType) && (string.IsNullOrEmpty(filterNameUser)))) {
				var allFindings = await findingService.GetAllFindings();
				return View(allFindings);
			}
			else {
				var filteredFindings = findingService.GetFilteredFindigns(filterName, filterNameUser, filterType);
				return View(filteredFindings);
			}
		}

	}

}
