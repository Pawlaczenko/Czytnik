using Czytnik.Services;
using Czytnik_Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace Czytnik.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<IActionResult> Profile()
        {
            UserProfileViewModel userInfo = await _userService.GetProfileInfo();
            return View(userInfo);
        }
        public async Task<IActionResult> Settings()
        {
            UserSettingsViewModel userInfo = await _userService.GetUserData();
            return View(userInfo);
        }
        public IActionResult Orders()
        {
            int ordersCount = _userService.GetOrders(-1, 0, "").Result.Count;
            return View(ordersCount);
        }
        public async Task<IActionResult> Reviews(string sortOrder)
        {
            int results = _userService.GetUserReviews(-1, sortOrder).Result.Count;
            return View(results);
        }
        public async Task<IActionResult> Favourites(string sortOrder)
        {
            var results = _userService.GetAllFavourites(0,-1, sortOrder).Result.Count;
            return View(results);
        }

        [HttpPost]
        public async Task<IActionResult> AddFavouriteBook(int bookId)
        {
            if(!Double.IsNaN(bookId) && !Double.IsInfinity(bookId))
            {
                await _userService.AddToFavourites(bookId);
                return Ok("{}");
            } else
            {
                return NotFound();
            }
            
        }

        [HttpGet]
        public async Task<JsonResult> GetAllUserFavourites(int skip = 0, int count = 5, string sortBy = "")
        {
            var reviewsViewModel = await _userService.GetAllFavourites(skip, count, sortBy);
            return Json(reviewsViewModel, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFavouriteBook(int bookId)
        {
            await _userService.DeleteFavourite(bookId);
            return Ok("{}");
        }
        [HttpGet]
        public async Task<JsonResult> GetAllOrders(int skip = 0, int count = 2, string sortBy = "")
        {
            var orders = await _userService.GetOrders(count, skip, sortBy);
            return Json(orders, new JsonSerializerOptions { PropertyNamingPolicy = null });
        }
        [HttpPost]
        public async Task<IActionResult> EditUser(UserSettingsViewModel userData)
        {
            if (!ModelState.IsValid)
            {
                TempData["error"] = "Coś poszło nie tak";
                return RedirectToAction("Settings");
            }

            await _userService.EditUserData(userData);
            return RedirectToAction("Settings");
        }

    }
}
