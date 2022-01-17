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

        public async Task<IEnumerable<BooksCarouselViewModel>> GetCarouselBooks(int count, int categoryId = -1)
        {
            Console.WriteLine(count);
            Console.WriteLine(categoryId);
            //var booksQuery = (categoryId>0)? _dbContext.Books.Where(b => b.Category.Id == categoryId) : _dbContext.Books;
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
            var booksQuery = _dbContext.Books.OrderByDescending(b => b.Rating).Select(b => new BestBooksViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Cover = b.Cover,
                Authors = b.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.SecondName} {ba.Author.Surname}").ToList()
            }).Take(3);
            var books = await booksQuery.ToListAsync();
            return books;
        }
    }
}
