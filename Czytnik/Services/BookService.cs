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
                ReviewCount = b.Reviews.Count(),
                Reviews = b.Reviews.OrderByDescending(el=>el.ReviewDate).Take(3).ToList(),
                Translators = b.BookTranslators.Select(ba => $"{ba.Translator.FirstName} {ba.Translator.SecondName} {ba.Translator.Surname}").ToList(),
                OriginalLanguage = b.OriginalLanguage.Name,
                EditionLanguage = b.EditionLanguage.Name
            }).FirstOrDefault();

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
    }
}
