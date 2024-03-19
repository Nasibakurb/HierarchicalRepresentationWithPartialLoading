using Azure;
using HierarchicalView.Domain.Interfaces.Category;
using HierarchicalView.Domain.ViewModel;
using HierarchicalView.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace HierarchicalView.Controllers
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
            var response = await _categoryService.GetAllCategories();
            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return View(response.Data);
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CreateCategoryModel model)
        {
            var response = await _categoryService.CreateCategory(model);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok) 
            {  
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(long id)
        {
            var response = await _categoryService.DeleteCategory(id);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            { 
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> EditCategory(CreateCategoryModel model)
        {
            var response = await _categoryService.EditCategory(model);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok) // Если объект создался
            { // Метод "ок" с объектом json c содерж. описание 
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }
    }
}