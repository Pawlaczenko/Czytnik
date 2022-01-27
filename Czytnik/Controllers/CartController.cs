using Microsoft.AspNetCore.Mvc;

namespace Czytnik.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
