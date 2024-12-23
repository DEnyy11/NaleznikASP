using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NálezníkASP.Models;
using System.Diagnostics;

namespace NálezníkASP.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private UserManager<AppUser> userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager) {
            _logger = logger;
            this.userManager = userManager;
        }
        [Authorize]
        public async Task<IActionResult> IndexAsync() {
            AppUser user = await userManager.GetUserAsync(HttpContext.User);
            //string message = $"Hello {user.UserName}";
            return View();
        }

        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            return View("NotFound");
        }
    }
}
