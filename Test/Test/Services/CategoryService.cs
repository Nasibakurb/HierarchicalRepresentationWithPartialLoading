using Test.Interfaces;
using Test.Models;
using Microsoft.Extensions.Logging;
using Test.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;


namespace Test.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly ISubcategoryRepository _subcategoryRepository;
        private ILogger<Category> _logger;
        public CategoryService(ICategoryRepository categoryRepository,
            ISubcategoryRepository subcategoryRepository,
            ILogger<Category> logger)
        {
            _categoryRepository = categoryRepository;
            _subcategoryRepository = subcategoryRepository;
            _logger = logger;

        }

        public async Task<List<Category>> CreateCategory(CreateCategoryModel model)
        {
            try
            {
                model.Validation();
                _logger.LogInformation($"Запрос на создание категории: {model.Name}");

                var categories = await _categoryRepository.GetAllCategories();
                var category = categories.FirstOrDefault(x => x.Name == model.Name);
                if (category != null)
                {
                    return new List<Category>();
                }
                var newCategory = new Category
                {
                    Name = model.Name
                };
                await _categoryRepository.Create(newCategory);
                _logger.LogInformation($"Создана новая категория: {model.Name}");
                return new List<Category> { newCategory };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при создании категории: {ex.Message}");
                return new List<Category>();
            }
        }

        public async Task<List<Category>> DeleteCategory(long id)
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategories();
                var category = categories.FirstOrDefault(x => x.Id == id);

                if (category == null)
                {
                    return new List<Category>();
                }
                await _categoryRepository.Delete(category);
                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[CategoryService.GetAllCategories]: {ex.Message}");
                return new List<Category>();
            }
        }

        public async Task<List<Category>> EditCategory(CreateCategoryModel model)
        {
            try {
                model.Validation();
                _logger.LogInformation($"Запрос на редактирование категории: {model.Name}");

                var categories = await _categoryRepository.GetAllCategories();
                var category = categories.FirstOrDefault(x => x.Id == model.Id);

                if (category == null)
                {
                    return new List<Category>();
                }

                category.Name = model.Name;

                await _categoryRepository.Update(category);
                _logger.LogInformation($"Категория успешно обновлена: {model.Id}");
                return categories;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[CategoryService.GetAllCategories]: {ex.Message}");
                return new List<Category>();
            }
        } 

        public async Task<List<Category>> GetAllCategories()
        {
            try
            {
                var categories = await _categoryRepository.GetAllCategories(); // Получаем все категории
                var resultCategory = new List<Category>(); // Создаем новый список для категорий с подкатегориями

                foreach (var category in categories) // Перебираем все категории
                {
                    var subcategories = await _subcategoryRepository.GetSubcategories(category.Id); // Получаем подкатегории для текущей категории
                    var categoryViewModel = new Category // Создаем объект Категории
                    {
                        Id = category.Id,
                        Name = category.Name,
                        Subcategories = subcategories
                    };
                    resultCategory.Add(categoryViewModel);
                }
                return resultCategory;
            }
            catch (Exception ex) 
            {
                _logger.LogInformation($"[CategoryService.GetAllCategories]: {ex.Message}");
                return new List<Category>();
            }
        }

    }
}
