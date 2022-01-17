using Czytnik_DataAccess.Database;
using Czytnik_Model.ViewModels;
using Microsoft.EntityFrameworkCore;
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

        public async Task<IEnumerable<BestOfMonthBooksViewModel>> GetBestOfMonthBooks()
        {
            var booksQuery = _dbContext.Books.OrderByDescending(b => b.NumberOfCopiesSold).Select(b => new BestOfMonthBooksViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                Cover = b.Cover,
                Rating = b.Rating,
                Category = b.Category,
                Authors = b.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.SecondName} {ba.Author.Surname}").ToList()
            }).Take(5);
            var books = await booksQuery.ToListAsync();
            return books;
        }
        public async Task<IEnumerable<BestOfMonthBooksViewModel>> GetBestOfAllTimeBooks()
        {
            var booksQuery = _dbContext.Books.OrderByDescending(b => b.Rating).Select(b => new BestOfMonthBooksViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                Cover = b.Cover,
                Rating = b.Rating,
                Category = b.Category,
                Authors = b.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.SecondName} {ba.Author.Surname}").ToList()
            }).Take(3);
            var books = await booksQuery.ToListAsync();
            return books;
        }
    }
}
