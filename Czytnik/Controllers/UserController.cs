using Czytnik.Services;
using Czytnik_Model.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
            return View();
        }
        public IActionResult Orders()
        {
            return View();
        }
        public async Task<IActionResult> Reviews(string sortOrder)
        {
            ViewData["RatingSortParm"] = String.IsNullOrEmpty(sortOrder) ? "rating_desc" : "rating";
            ViewData["DateSortParm"] = sortOrder == "Date" ? "date_desc" : "date";

            var results = await _userService.GetUserReviews(-1, sortOrder);
            return View(results);
        }
        public IActionResult Favourites()
        {
            return View();
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

        [HttpDelete]
        public async Task<IActionResult> DeleteFavouriteBook(int bookId)
        {
            await _userService.DeleteFavourite(bookId);
            return Ok("{}");
        }

    }
}
