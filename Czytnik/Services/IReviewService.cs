using Czytnik_Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Czytnik.Services
{
    public interface IReviewService
    {
        Task<IEnumerable<ReviewViewModel>> GetAll(int Id);
    }
}
