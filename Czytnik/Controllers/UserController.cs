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
        public IActionResult Settings()
        {
            return View();
        }
        public IActionResult Orders()
        {
            return View();
        }
        public IActionResult Templates()
        {
            return View();
        }
        public IActionResult Reviews()
        {
            return View();
        }
        public IActionResult Favourites()
        {
            return View();
        }
    }
}
