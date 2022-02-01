using Czytnik_Model.Models;
using Czytnik_Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Czytnik.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemsViewModel>> GetCartItems(string userId);
        void DeleteCartItem(int bookId, string userId);
        void AddCartItem(int bookId, string userId);
        void UpdateQuantity(int bookId, string userId, short quantity);
        Task<int> GetCartQuantity(string userId);
    }
}
