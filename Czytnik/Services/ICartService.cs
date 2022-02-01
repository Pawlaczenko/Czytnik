using Czytnik_Model.Models;
using Czytnik_Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Czytnik.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemsViewModel>> GetCartItems();
        void DeleteCartItem(int bookId);
        void AddCartItem(int bookId);
        void UpdateQuantity(int bookId, short quantity);
        Task<int> GetCartQuantity();
    }
}
