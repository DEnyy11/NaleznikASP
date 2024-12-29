using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NálezníkASP.DTO;
using NálezníkASP.Models;
using NálezníkASP.Services;

namespace NálezníkASP.Controllers {
    public class DashboardController : Controller {
        private UserManager<AppUser> userManager;
        private DashboardService dashboardService;
        public DashboardController(DashboardService dashboardService, UserManager<AppUser> userManager) {
            this.dashboardService = dashboardService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> IndexAsync() {
            var user = await userManager.GetUserAsync(HttpContext.User);
            if (user == null) {
                return RedirectToAction("Login", "Account"); 
            }
            
            var data = await dashboardService.GetDashboardData(user);
            return View(data);
        }
    }
}
