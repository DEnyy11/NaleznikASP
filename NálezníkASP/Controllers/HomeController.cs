using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NálezníkASP.Models;
using System.Diagnostics;
using NálezníkASP.Services;
using NálezníkASP.DTO;

namespace NálezníkASP.Controllers {
    public class HomeController : Controller {
        private readonly ILogger<HomeController> _logger;
        private UserManager<AppUser> userManager;
        private FindingService findingService;
        

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager) {
            _logger = logger;
            this.userManager = userManager;
        }
        [Authorize]
        public IActionResult Index() {
            return View();
        }
        
        public IActionResult Privacy() {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
           
        }
        public IActionResult Error404() {
            return View("NotFound");
        }

    }
}
