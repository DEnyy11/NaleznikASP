using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using NálezníkASP.Services;
using NálezníkASP.DTO;

namespace NálezníkASP.Controllers {
    public class FindingController : Controller {
        private FindingService findingService;

        public FindingController(FindingService findingService) {
            this.findingService = findingService;
        }


        [HttpGet]
        public IActionResult Index() {
            return View();
        }

        public IActionResult Create() {
            return RedirectToAction("Index");
        }

        public IActionResult NewCoin() {
            return View();
        }
        public IActionResult NewArtifact() {
            return View();
        }

    }

}
