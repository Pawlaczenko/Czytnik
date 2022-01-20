using Czytnik.Services;
using Czytnik_Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace Czytnik.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IReviewService _reviewService;

        public BookController(IBookService bookService, IReviewService reviewService)
        {
            _bookService = bookService;
            _reviewService = reviewService;
        }
        public IActionResult Index(int Id)
        {
            var books = _bookService.GetProductBookPage(Id);
            return View(books);
        }

        public IActionResult ReviewsList(int Id)
        {
            //dynamic reviewList = new ExpandoObject();
            //reviewList.reviews = await _reviewService.GetAll(Id);
            //reviewList.title = title;
            //reviewList.bookId = Id;
            //return View(reviewList);
            var reviewView = _reviewService.GetReviewList(Id);
            return View(reviewView);
        }
    }
}
