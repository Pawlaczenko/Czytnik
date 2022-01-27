using Czytnik.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Czytnik.Controllers
{
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public IActionResult Index()
        {
            int userId = 1; //For now
            var cartItems = _cartService.GetCartItems(userId);
            return View(cartItems.Result);
        }

        [HttpDelete]
        public JsonResult DeleteItem(int bookId, int userId)
        {
            _cartService.DeleteCartItem(bookId, userId);
            return Json(null);
        }
    }
}
