using Microsoft.AspNetCore.Mvc;

namespace NálezníkASP.Controllers {
    public class ProfileController : Controller {
        public IActionResult Index() {
            return View();
        }
    }
}
