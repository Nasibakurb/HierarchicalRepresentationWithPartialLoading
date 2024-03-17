using Microsoft.AspNetCore.Mvc;
using Test.Interfaces;
using Test.Models;
using Test.Models.ViewModel;
using Test.Services;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICategoryService _categoryService;

        public HomeController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllCategories();
            return View(categories);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryModel model)
        {
            var response = await _categoryService.CreateCategory(model);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCategory(long id)
        {
            var response = await _categoryService.DeleteCategory(id);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(CreateCategoryModel model)
        {
            var response = await _categoryService.EditCategory(model);
            return Ok(response);
        }


    }
}