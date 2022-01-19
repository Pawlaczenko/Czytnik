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

        public async Task<JsonResult> GetAllFilters()
        {
            var categoriesViewModel = await _categoryService.GetAll();
            var languagesViewModel = await _languageService.GetAll();

            var filtersResult = new { categories = categoriesViewModel, languages = languagesViewModel };

            return Json(filtersResult);
        }
    }
}
