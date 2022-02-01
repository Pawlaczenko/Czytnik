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
            var cartItems = _cartService.GetCartItems();
            return View(cartItems.Result);
        }

        [HttpDelete]
        public JsonResult DeleteItem(int bookId)
        {
            _cartService.DeleteCartItem(bookId);
            return Json(null);
        }

        [HttpPost]
        public IActionResult AddItem(int bookId)
        {
            _cartService.AddCartItem(bookId);
            return Ok("{}");
        }
        
        [HttpPatch]
        public IActionResult UpdateQuantity(int bookId, short quantity)
        {
            _cartService.UpdateQuantity(bookId, quantity);
            return Ok();
        }
    }
}
