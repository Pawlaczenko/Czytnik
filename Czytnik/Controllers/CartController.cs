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
        public JsonResult DeleteItem(int bookId)
        {
            int userId = 1;
            _cartService.DeleteCartItem(bookId, userId);
            return Json(null);
        }

        [HttpPost]
        public IActionResult AddItem(int bookId)
        {
            int userId = 1;
            _cartService.AddCartItem(bookId, userId);
            return Ok("{}");
        }
        
        [HttpPatch]
        public IActionResult UpdateQuantity(int bookId, short quantity)
        {
            int userId = 1;
            _cartService.UpdateQuantity(bookId, userId, quantity);
            return Ok();
        }
    }
}
