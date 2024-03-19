using HierarchicalView.Domain.Entity;
using HierarchicalView.Domain.Enum;
using HierarchicalView.Domain.Interfaces.Category;
using HierarchicalView.Domain.Interfaces.Subcategory;
using HierarchicalView.Domain.Response;
using HierarchicalView.Domain.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalView.Infrastructure.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private ILogger<CategoryEntity> _logger;
        public CategoryService(ICategoryRepository categoryRepository,
            ISubcategoryRepository subcategoryRepository,
            ILogger<CategoryEntity> logger)
        {
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _logger = logger;

        }

        public async Task<IBaseResponse<CategoryEntity>> CreateCategory(CreateCategoryModel model)
        {
            try
            {
                model.Validation();
                _logger.LogInformation($"Запрос на создание категории: {model.Name}");

                var categories = await _categoryRepository.GetAllCategories();
                var category = categories.FirstOrDefault(x => x.Name == model.Name);
                if (category != null)
                {
                    return new BaseResponse<CategoryEntity>() // Создается новый объект
                    {
                        Description = "Категория с таким названием уже есть", // Описание ошибки
                        StatusCode = StatusCode.EntityIsHasAlready // Код состоян. ошибки 
                    };
                }
                var newCategory = new CategoryEntity
                {
                    Name = model.Name
                };
                await _categoryRepository.Create(newCategory);

                _logger.LogInformation($"Создана новая категория: {model.Name}");
                return new BaseResponse<CategoryEntity>()
                { // Возвр. объект с информацией 
                    Description = "Данная задача создалась",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при создании категории: {ex.Message}");
                return new BaseResponse<CategoryEntity>()
                {
                    Description = $"Ошибка: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<CategoryEntity>> DeleteCategory(long id)
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategories();
                var category = categories.FirstOrDefault(x => x.Id == id);

                if (category == null)
                {
                    return new BaseResponse<CategoryEntity>()
                    {
                        Description = $"Задача не найдена",
                        StatusCode = StatusCode.EntitykNotFound
                    };
                }
                await _categoryRepository.Delete(category);
                return new BaseResponse<CategoryEntity>()
                {
                    Description = $"Задача удалена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[CategoryService.DeleteCategory]: {ex.Message}");
                return new BaseResponse<CategoryEntity>()
                {
                    Description = $"Ошибка: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<CategoryEntity>> EditCategory(CreateCategoryModel model)
        {
            try
            {
                model.Validation();
                _logger.LogInformation($"Запрос на редактирование категории: {model.Name}");

                var categories = await _categoryRepository.GetAllCategories();
                var category = categories.FirstOrDefault(x => x.Id == model.Id);

                if (category == null)
                {
                    return new BaseResponse<CategoryEntity>()
                    {
                        Description = "Задача не найдена",
                        StatusCode = StatusCode.EntitykNotFound
                    };
                }

                category.Name = model.Name;

                await _categoryRepository.Update(category);
                _logger.LogInformation($"Категория успешно обновлена: {model.Id}");
                return new BaseResponse<CategoryEntity>()
                {
                    Description = "Задача успешно обновлена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[CategoryService.GetAllCategories]: {ex.Message}");
                return new BaseResponse<CategoryEntity>()
                {
                    Description = $"Ошибка: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<List<CategoryEntity>>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategories(); // Получаем все категории
                var resultCategory = new List<CategoryEntity>(); // Создаем новый список для категорий с подкатегориями

                foreach (var category in categories) // Перебираем все категории
                {
                    var subcategories = await _subcategoryRepository.GetSubcategories(category.Id); // Получаем подкатегории для текущей категории
                    var categoryViewModel = new CategoryEntity // Создаем объект Категории
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Subcategories = subcategories
                    };
                    resultCategory.Add(categoryViewModel);
                }
                return new BaseResponse<List<CategoryEntity>>()
                {
                    Data = resultCategory, // Возвращаем список категорий как часть ответа
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[CategoryService.GetAllCategories]: {ex.Message}");
                return new BaseResponse<List<CategoryEntity>>()
                {
                    Description = $"Ошибка: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

    }
}
    

