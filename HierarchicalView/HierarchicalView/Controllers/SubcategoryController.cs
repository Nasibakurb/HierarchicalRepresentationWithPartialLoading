using HierarchicalView.Domain.Interfaces.Subcategory;
using HierarchicalView.Domain.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HierarchicalView.Controllers
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
            ViewData["CategoryEntityId"] = categoryId; // Передаем categoryId в представление через ViewData
            return PartialView("CreateSubcategoryPartial", model);
        }


        [HttpPost]
        public async Task<IActionResult> Create(CreateSubcategoryModel model, int categoryId)
        {
            var response = await _service.CreateSubcategory(model, categoryId);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }


        [HttpPost]
        public async Task<IActionResult> Delete(long id, int categoryId)
        {
            var response = await _service.DeleteSubcategoryy(id, categoryId);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok)
            {
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CreateSubcategoryModel model, int categoryId)
        {
            var response = await _service.EditSubcategory(model, categoryId);

            if (response.StatusCode == Domain.Enum.StatusCode.Ok) // Если объект создался
            { // Метод "ок" с объектом json c содерж. описание 
                return Ok(new { description = response.Description });
            }
            return BadRequest(new { description = response.Description });
        }

        public IActionResult EditSubcategoryPartial(int categoryId, int subcategoryId)
        {
            var model = new CreateSubcategoryModel();
            ViewData["CategoryEntityId"] = categoryId;
            ViewData["Id"] = subcategoryId;
            return PartialView("EditSubcategoryPartial", model);
        }


    }
}
