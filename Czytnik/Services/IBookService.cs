using Czytnik_Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Czytnik.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BooksCarouselViewModel>> GetTopMonthBooks(int count, DateTime date);
        Task<IEnumerable<BestBooksViewModel>> GetBestOfAllTimeBooks();
        Task<IEnumerable<BooksCarouselViewModel>> GetSimilarBooks(int seriesId, int categoryId, int bookId);
        ProductPageViewModel GetProductBookPage(int bookId);

    }
}
