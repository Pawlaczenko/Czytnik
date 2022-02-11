using Czytnik_Model.Models;
using Czytnik_Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Czytnik.Services
{
    public interface IUserService
    {
       Task<UserProfileViewModel> GetProfileInfo();
       Task<bool> DidUserRateThisBook(int bookId);
        Task<bool> DidUserBefriendThisBook(int bookId);
        Task<List<UserReviewViewModel>> GetUserReviews(int count, string sortOrder);
        Task AddToFavourites(int bookId);
        Task DeleteFavourite(int bookId);
        Task<List<BestBooksViewModel>> GetAllFavourites(int count, string sortBy);
    }
}
