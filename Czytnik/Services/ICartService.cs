using Czytnik_Model.Models;
using Czytnik_Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Czytnik.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemsViewModel>> GetCartItems(int userId);
        void DeleteCartItem(int bookId, int userId);
        void AddCartItem(int bookId, int userId);
        void UpdateQuantity(int bookId, int userId, short quantity);
    }
}
