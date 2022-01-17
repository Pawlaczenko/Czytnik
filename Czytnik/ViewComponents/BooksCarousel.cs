using Czytnik.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

public class BooksCarousel : ViewComponent
{
    private readonly IBookService _bookService;

    public BooksCarousel(IBookService bookService)
    {
        _bookService = bookService;
    }
    public async Task<IViewComponentResult> InvokeAsync(int count, int category)
    {
        var books = await _bookService.GetCarouselBooks(count,category);
        string viewName = (category < 0) ? "Default" : "Product";
        return View(viewName, books);
    }
}
