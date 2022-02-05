using Czytnik_Model.Models;
using Czytnik_Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Czytnik.Controllers.CheckoutController;

namespace Czytnik.Services
{
  public interface ICheckoutService
  {
    Task<decimal> CalculatePrice(Item[] items);
  }
}
