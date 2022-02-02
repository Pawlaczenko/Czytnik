﻿using System.Threading.Tasks;
using Czytnik_Model.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace Czytnik.Areas.Identity.Pages.Account.Manage
{
    public class ProfileModel : PageModel
    {
        private readonly UserManager<User> _userManager;
        private readonly ILogger<ProfileModel> _logger;

        public ProfileModel(
            UserManager<User> userManager,
            ILogger<ProfileModel> logger)
        {
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Nie udało się załadować użytkownika o id '{_userManager.GetUserId(User)}'.");
            }

            return Page();
        }
    }
}