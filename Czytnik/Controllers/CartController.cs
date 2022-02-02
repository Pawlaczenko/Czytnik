using Czytnik.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Czytnik.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        public async Task<IActionResult> Index()
        {
            var cartItems = await _cartService.GetCartItems();
            return View(cartItems);
        }

        [HttpDelete]
        public async Task<JsonResult> DeleteItem(int bookId)
        {
            await _cartService.DeleteCartItem(bookId);
            return Json(null);
        }

        [HttpPost]
        public async Task<IActionResult> AddItem(int bookId)
        {
            await _cartService.AddCartItem(bookId);
            return Ok("{}");
        }

        [HttpPatch]
        public async Task<IActionResult> UpdateQuantity(int bookId, short quantity)
        {
            await _cartService.UpdateQuantity(bookId, quantity);
            return Ok();
        }
    }
}
