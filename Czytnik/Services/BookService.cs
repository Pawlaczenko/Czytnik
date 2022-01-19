using Czytnik_DataAccess.Database;
using Czytnik_Model.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Czytnik.Services
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _dbContext;
        public BookService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ProductPageViewModel GetProductBookPage(int bookId)
        {
            var BookObject = _dbContext.Books.Where(b => b.Id == bookId);
            var top99books = _dbContext.Books.OrderByDescending(entry => entry.NumberOfCopiesSold).Select(entry => entry.Id).Take(99).ToList();
            int bestseller = top99books.FindIndex(i => i == bookId) +1; // 0 -nie ma go na liscie, 1-99 - ranking na liscie

            var bookQuery = BookObject.Select(b => new ProductPageViewModel
            {
                Product = b,
                Authors = b.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.SecondName} {ba.Author.Surname}").ToList(),
                Discount = b.BookDiscounts.Where(entry => entry.BookId == b.Id).Select(entry => entry.Discount).FirstOrDefault(),
                Bestseller = bestseller,
                Publisher = b.Publisher.Name,
                Category = b.Category,
                ReviewCount = b.Reviews.Count,
                Translators = b.BookTranslators.Select(ba => $"{ba.Translator.FirstName} {ba.Translator.SecondName} {ba.Translator.Surname}").ToList(),
                OriginalLanguage = b.OriginalLanguage.Name,
                EditionLanguage = b.EditionLanguage.Name,
            }).FirstOrDefault();

            //inaczej nie działa zagnieżdżony Select jeśli chce użyć w środku .Take(), bo ludzie od asp.net core byli zbyt leniwi żeby to naprawić przed releasem asp.net 6.0
            bookQuery.Reviews = (from review in _dbContext.Reviews
                                where review.BookId == bookId
                                select new ReviewViewModel
                                {
                                    Id = review.Id,
                                    Rating = review.Rating,
                                    Username = review.User.Username,
                                    ReviewText = review.ReviewText,
                                    ReviewDate = review.ReviewDate
                                }).OrderByDescending(r=>r.ReviewDate).Take(3).ToList();
            bookQuery.CalculatedPrice = (bookQuery.Discount == null) ? bookQuery.Product.Price : CalculateDiscount(bookQuery.Product.Price,bookQuery.Discount.DiscountValue);
            return bookQuery;
        }

        public async Task<IEnumerable<BooksCarouselViewModel>> GetCarouselBooks(int count, int categoryId = -1)
        {
            IQueryable<BooksCarouselViewModel> booksQuery = _dbContext.Books.OrderByDescending(b => b.NumberOfCopiesSold).Select(b => new BooksCarouselViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                Cover = b.Cover,
                Rating = b.Rating,
                Category = b.Category,
                Authors = b.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.SecondName} {ba.Author.Surname}").ToList()
            });
            if (categoryId > 0)
            {
                booksQuery = booksQuery.Where(b => b.Category.Id == categoryId);
            }    

            var books = await booksQuery.Take(count).ToListAsync();
            return books;
        }

        public async Task<IEnumerable<BestBooksViewModel>> GetBestOfAllTimeBooks()
        {
            var booksQuery = _dbContext.Books.OrderByDescending(b => b.Rating*b.NumberOfCopiesSold).Select(b => new BestBooksViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Cover = b.Cover.Replace("-w-iext","-b-iext"),
                Authors = b.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.SecondName} {ba.Author.Surname}").ToList()
            }).Take(3);
            var books = await booksQuery.ToListAsync();
            return books;
        }

        private decimal CalculateDiscount(decimal price, int discount)
        {
            var discountPercentage = ((decimal)discount / 100);
            return Math.Round(price * discountPercentage, 2);
        }
    }
}
