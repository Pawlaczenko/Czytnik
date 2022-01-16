using Czytnik_Model.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Czytnik.Services
{
    public interface IBookService
    {
        Task<IEnumerable<BestOfMonthBooksViewModel>> GetBestOfMonthBooks();
        Task<IEnumerable<BestOfMonthBooksViewModel>> GetBestOfAllTimeBooks();

    }
}
