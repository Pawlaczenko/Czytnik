using Czytnik_Model.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Czytnik.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BooksCarouselViewModel>> GetCarouselBooks(int count, int categoryId);
        Task<IEnumerable<BestBooksViewModel>> GetBestOfAllTimeBooks();
        ProductPageViewModel GetProductBookPage(int bookId);

    }
}
