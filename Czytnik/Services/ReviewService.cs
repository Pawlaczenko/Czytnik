using Czytnik_DataAccess.Database;
using Czytnik_Model.Models;
using Czytnik_Model.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Czytnik.Services
{
    public class ReviewService : IReviewService
    {
        private readonly AppDbContext _dbContext;
        public ReviewService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ReviewViewModel>> GetAll(int Id)
        {
            var reviews = _dbContext.Reviews
                .Where(r => r.BookId == Id)
                .Select(r => new ReviewViewModel
                {
                    Id = r.Id,
                    Rating = r.Rating,
                    Username = r.User.Username,
                    ReviewText = r.ReviewText,
                    ReviewDate = r.ReviewDate,
                })
                .OrderByDescending(r => r.ReviewDate);
            var result = await reviews.ToListAsync();
            return result;
        }
    }
}
