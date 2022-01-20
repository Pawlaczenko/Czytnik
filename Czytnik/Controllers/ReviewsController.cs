using Czytnik.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Czytnik.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly IReviewService _reviewService;
        public ReviewsController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public async Task<JsonResult> GetAllReviews(int Id, int skip=0, int count=10)
        {
            var reviewsViewModel = await _reviewService.GetAll(Id,skip,count);
            return Json(reviewsViewModel);
        }
    }
}
