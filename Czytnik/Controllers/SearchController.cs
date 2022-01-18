using Czytnik.Services;
using Microsoft.AspNetCore.Mvc;
using System.Dynamic;
using System.Threading.Tasks;

namespace Czytnik.Controllers
{
    public class SearchController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly ILanguageService _languageService;
        public SearchController(ICategoryService categoryService, ILanguageService languageService)
        {
            _categoryService = categoryService;
            _languageService = languageService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            dynamic filteringData = new ExpandoObject();

            filteringData.categories = await _categoryService.GetAll();
            filteringData.languages = await _languageService.GetAll();
            return View(filteringData);
        }
    }
}
