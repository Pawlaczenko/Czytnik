using Czytnik_Model.Models;
using Czytnik_Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Czytnik.Services
{
    public interface IUserService
    {
       Task<UserProfileViewModel> GetProfileInfo();
       Task<bool> DidUserRateThisBook(int bookId);
    }
}
