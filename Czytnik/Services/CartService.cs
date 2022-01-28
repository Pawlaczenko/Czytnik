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
    public class CartService : ICartService
    {
        private readonly AppDbContext _dbContext;
        public CartService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<CartItemsViewModel>> GetCartItems(int userId)
        {
            var itemsQuery = _dbContext.CartItems.Where(i => i.UserId == userId).Select(i => new CartItemsViewModel
            {
                bookId = i.BookId,
                userId = i.UserId,
                Title = i.Book.Title,
                Price = i.Book.Price,
                Cover = i.Book.Cover,
                Quantity = i.Quantity,
                Authors = i.Book.BookAuthors.Select(ba => $"{ba.Author.FirstName} {ba.Author.SecondName} {ba.Author.Surname}").ToList(),
                Discount = i.Book.BookDiscounts.Where(entry => entry.BookId == i.BookId).Select(entry => entry.Discount).FirstOrDefault(),
            });

            IEnumerable<CartItemsViewModel> result = await itemsQuery.ToListAsync();

            foreach (var item in result)
            {
                item.CalculatedPrice = (item.Discount == null) ? item.Price : CalculateDiscount(item.Price, item.Discount.DiscountValue);
                item.FullPrice = item.CalculatedPrice * item.Quantity;
            }

            return result;
        }

        public void DeleteCartItem(int bookId, int userId)
        {
            var cartItem = _dbContext.CartItems.Where(i => i.BookId == bookId && i.UserId == userId).First();
            _dbContext.CartItems.Remove(cartItem);
            _dbContext.SaveChangesAsync();
        }

        public void AddCartItem(int bookId, int userId)
        {
            var item = new CartItem { BookId = bookId, UserId = userId, Quantity = 1 };
            _dbContext.Add(item);
            _dbContext.SaveChanges();
        }
        
        public void UpdateQuantity(int bookId, int userId, short quantity)
        {
            var item = _dbContext.CartItems.Where(i => i.BookId == bookId && i.UserId == userId).First();
            item.Quantity = quantity;
            _dbContext.SaveChanges();
        }

        private decimal CalculateDiscount(decimal price, int discount)
        {
            var discountPercentage = ((decimal)discount / 100);
            return Math.Round(price * discountPercentage, 2);
        }
    }
}
