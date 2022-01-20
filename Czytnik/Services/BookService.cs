﻿using Czytnik_DataAccess.Database;
using Czytnik_Model.Models;
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
                Series = b.Series
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

        public async Task<IEnumerable<BooksCarouselViewModel>> GetTopMonthBooks(int count, DateTime date)
        {
            var today = new DateTime(date.Year, date.Month, 1);
            var firstDayOfMonth = today.AddMonths(-1);
            var lastDayOfMonth = today.AddDays(-1);

            var monthBooksQuery = _dbContext.OrderItems
                .Select(x => new
                {
                    x.Book.Id,
                    x.Quantity,
                    x.Order.OrderDate
                })
                .Where(el => el.OrderDate >= firstDayOfMonth && el.OrderDate <= lastDayOfMonth)
                .GroupBy(x => new { x.Id})
                .Select(x => new
                {
                    Id = x.Key.Id,
                    qntSum = x.Sum(y => y.Quantity)
                })
                .OrderByDescending(el=>el.qntSum)
                .Take(count)
                .Select(i => i.Id)
                .ToList();

            var res = _dbContext.Books.Where(b => monthBooksQuery.Contains(b.Id))
                .Select(b => new BooksCarouselViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    Cover = b.Cover,
                    Rating = b.Rating,
                    Category = b.Category,
                    Authors = b.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.SecondName} {ba.Author.Surname}").ToList()
                });
            return res;
        }

        public async Task<IEnumerable<BooksCarouselViewModel>> GetSimilarBooks(int seriesId, int categoryId, int bookId)
        {
            var booksQuery = _dbContext.Books.Where(b => b.Id != bookId && (b.SeriesId == seriesId || b.CategoryId==categoryId)).Select(b => new BooksCarouselViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                Cover = b.Cover,
                Rating = b.Rating,
                Category = b.Category,
                Authors = b.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.SecondName} {ba.Author.Surname}").ToList()
            });
            var result = await booksQuery.Take(4).ToListAsync();
            return result;
        }

        public async Task<IEnumerable<BooksSearchViewModel>> SearchBooks(Search search)
        {
            IQueryable<Book> booksQueryBuilder = _dbContext.Books;

            if (search.CategoryId != null)
                booksQueryBuilder = booksQueryBuilder.Where(b => b.CategoryId == search.CategoryId);

            if (search.LanguageId != null)
                booksQueryBuilder = booksQueryBuilder.Where(b => b.EditionLanguageId == search.LanguageId);

            if (search.StartPrice != null)
                booksQueryBuilder = booksQueryBuilder.Where(b => b.Price >= search.StartPrice);

            if (search.EndPrice != null)
                booksQueryBuilder = booksQueryBuilder.Where(b => b.Price <= search.EndPrice);

            if (search.StartDate != null)
                booksQueryBuilder = booksQueryBuilder.Where(b => b.ReleaseDate >= search.StartDate);

            if (search.EndDate != null)
                booksQueryBuilder = booksQueryBuilder.Where(b => b.ReleaseDate <= search.EndDate);

            if (search.SearchText != null)
                booksQueryBuilder = booksQueryBuilder.Where(b => b.Title.Contains(search.SearchText));

            if (search.Sort == "alphabet")
                booksQueryBuilder = booksQueryBuilder.OrderBy(b => b.Title);

            if (search.Sort == "price-down")
                booksQueryBuilder = booksQueryBuilder.OrderByDescending(b => b.Price);

            if (search.Sort == "price-up")
                booksQueryBuilder = booksQueryBuilder.OrderBy(b => b.Price);

            if (search.Sort == "date-down")
                booksQueryBuilder = booksQueryBuilder.OrderByDescending(b => b.ReleaseDate);

            if (search.Sort == "date-up")
                booksQueryBuilder = booksQueryBuilder.OrderBy(b => b.ReleaseDate);
            
            if (search.Sort == "rating-down")
                booksQueryBuilder = booksQueryBuilder.OrderByDescending(b => b.Rating);

            if (search.Sort == "rating-up")
                booksQueryBuilder = booksQueryBuilder.OrderBy(b => b.Rating);
            
            

            var booksQuery = booksQueryBuilder.Select(b => new BooksSearchViewModel
            {
                Id = b.Id,
                Title = b.Title,
                Price = b.Price,
                Cover = b.Cover,
                Rating = b.Rating,
                Category = b.Category,
                Discount = b.BookDiscounts.Where(entry => entry.BookId == b.Id).Select(entry => entry.Discount).FirstOrDefault(),
                Authors = b.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.SecondName} {ba.Author.Surname}").ToList(),
            }).Take(30);
            
            var result =  await booksQuery.ToListAsync();

            foreach (var book in result)
            {
                book.CalculatedPrice = (book.Discount == null) ? book.Price : CalculateDiscount(book.Price, book.Discount.DiscountValue);
            }

            return result;
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
