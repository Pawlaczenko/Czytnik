using Czytnik_DataAccess.Database;
using Czytnik_Model.Models;
using Czytnik_Model.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Czytnik.Services
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<User> _userManager;

        public UserService(AppDbContext dbContext, IHttpContextAccessor accessor, UserManager<User> userManager)
        {
            _dbContext = dbContext;
            _httpContextAccessor = accessor;
            _userManager = userManager;
        }

        public async Task<UserProfileViewModel> GetProfileInfo()
        {
            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            UserProfileViewModel userInfoModel = new UserProfileViewModel
            {
                FirstName = currentUser.FirstName,
                SecondName = currentUser.SecondName,
                Surname = currentUser.Surname,
                Birthdate = currentUser.BirthDate,
                Email = currentUser.Email,
                PhoneNumber = currentUser.PhoneNumber,
                ProfilePicture = currentUser.ProfilePicture,
                Username = currentUser.UserName
            };
            return userInfoModel;
        }

        public async Task<bool> DidUserRateThisBook(int bookId)
        {
            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var review = _dbContext.Reviews.Where(r => r.User == currentUser).Where(r => r.BookId == bookId).FirstOrDefault();
            return review == null; //może - true, nie 
        }
    }
}
