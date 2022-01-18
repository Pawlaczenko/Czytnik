using Microsoft.AspNetCore.Mvc;

namespace Czytnik.Controllers
{
    public class SearchController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
