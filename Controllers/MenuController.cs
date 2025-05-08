using Microsoft.AspNetCore.Mvc;

namespace FGC_PWA2025.Controllers
{
    public class MenuController : Controller
    {
        public IActionResult Menu()
        {
            return View();
        }
    }
}
