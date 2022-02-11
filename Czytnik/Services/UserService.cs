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
                Username = currentUser.UserName,
                UserReviews = GetUserReviews(4,"").Result,
                Favourites = GetAllFavourites(0,4,"").Result,
            };
            return userInfoModel;
        }

        public async Task<List<UserReviewViewModel>> GetUserReviews(int count, string sortOrder)
        {
            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var reviews = _dbContext.Reviews.Where(r => r.User == currentUser).Select(r => new UserReviewViewModel
            {
                Id = r.Id,
                Rating = r.Rating,
                ReviewText = r.ReviewText,
                ReviewDate = r.ReviewDate,
                BookTitle = r.Book.Title,
                Authors = r.Book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.SecondName} {ba.Author.Surname}").ToList(),
                BookId = r.BookId
            });

            switch (sortOrder)
            {
                case "rating_desc":
                    reviews = reviews.OrderByDescending(r => r.Rating);
                    break;
                case "rating":
                    reviews = reviews.OrderBy(r => r.Rating);
                    break;
                case "date_desc":
                    reviews = reviews.OrderByDescending(r => r.ReviewDate);
                    break;
                default:
                    reviews = reviews.OrderBy(r => r.ReviewDate);
                    break;
            }

            if (count > 0) reviews = reviews.Take(count); 

            var results = await reviews.ToListAsync();
            return results;
        }

        public async Task<bool> DidUserRateThisBook(int bookId)
        {
            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var review = _dbContext.Reviews.Where(r => r.User == currentUser).Where(r => r.BookId == bookId).FirstOrDefault();
            return review == null; //może - true, nie 
        }

        public async Task<bool> DidUserBefriendThisBook(int bookId)
        {
            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var fav = _dbContext.Favourites.Where(f => f.User == currentUser).Where(f => f.BookId == bookId).FirstOrDefault();
            return fav == null; // - true, nie 
        }

        public async Task AddToFavourites(int bookId)
        {
            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (currentUser != null && DidUserBefriendThisBook(bookId).Result)
            {
                var fav = new Favourite
                {
                    BookId = bookId,
                    User = currentUser
                };
                await _dbContext.Favourites.AddAsync(fav);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteFavourite(int bookId)
        {
            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var favourite = _dbContext.Favourites.Where(f => f.BookId == bookId && f.User==currentUser).FirstOrDefault();
            if (currentUser != null && favourite!=null)
            {
                _dbContext.Favourites.Remove(favourite);
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task<List<BestBooksViewModel>> GetAllFavourites(int skip = 0, int count = 5, string sortBy = "")
        {
            var currentUser = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            var favourites = _dbContext.Favourites.Where(f => f.User == currentUser).Select(f => new BestBooksViewModel
            {
                Cover = f.Book.Cover,
                Title = f.Book.Title,
                Id = f.Book.Id,
                Authors = f.Book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.SecondName} {ba.Author.Surname}").ToList(),
            });

            switch(sortBy)
            {
                case "title_desc":
                    favourites = favourites.OrderByDescending(f => f.Title);
                    break;
                default:
                    favourites = favourites.OrderBy(f => f.Title);
                    break;
            }

            if (count > 0) favourites = favourites.Skip(skip).Take(count);

            var results = await favourites.ToListAsync();
            return results;
        }
    }
}
