using Microsoft.AspNetCore.Mvc;
using Test.Interfaces;
using Test.Models;
using Test.Models.ViewModel;
using Test.Services;

namespace Test.Controllers
{
    public class SubcategoryController : Controller
    {
        private readonly ISubcategoryService _service;

        public SubcategoryController(ISubcategoryService service)
        {
            _service = service;
        }

        public IActionResult CreateSubcategoryPartial(int categoryId)
        {
            var model = new CreateSubcategoryModel();
            ViewData["CategoryId"] = categoryId; // Передаем categoryId в представление через ViewData
            return PartialView("CreateSubcategoryPartial", model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateSubcategoryModel model, int categoryId)
        {
            var response = await _service.CreateSubcategory(model, categoryId);
            return Ok(response);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(long id, int categoryId)
        {
            var response = await _service.DeleteSubcategoryy(id, categoryId);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateSubcategoryModel model, int categoryId)
        {
            var response = await _service.EditSubcategory(model, categoryId);
            return Ok(response);
        }

        public IActionResult EditSubcategoryPartial(int categoryId, int subcategoryId)
        {
            var model = new CreateSubcategoryModel();
            ViewData["CategoryId"] = categoryId;
            ViewData["Id"] = subcategoryId;
            return PartialView("EditSubcategoryPartial", model);
        }


    }
}
