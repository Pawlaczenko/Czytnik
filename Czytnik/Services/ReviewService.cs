using Czytnik_DataAccess.Database;
using Czytnik_Model.Models;
using Czytnik_Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
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

        public async Task<IEnumerable<ReviewViewModel>> GetAll(int Id, int skip=0, int count=10)
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
            var result = await reviews.Skip(skip).Take(count).ToListAsync();
            return result;
        }

        public ReviewListViewModel GetReviewList(int BookId)
        {
            var reviewList = _dbContext.Books
                .Where(b => b.Id == BookId)
                .Select(b => new ReviewListViewModel
                {
                    BookId = BookId,
                    Title = b.Title,
                    ReviewCount = b.Reviews.Count,
                    Rating = b.Rating
                }).SingleOrDefault();

            var reviewCount = _dbContext.Reviews
                .Where(el => el.BookId == BookId)
                .GroupBy(r => r.Rating)
                .Select(g => new { rating = g.Key, count = g.Count() }).ToList();

            reviewList.ReviewsQnt = reviewCount.ToDictionary(x=>x.rating,x=>x.count);

            return reviewList ;
        }
    }
}
