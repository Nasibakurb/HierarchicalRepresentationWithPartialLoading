using Test.Interfaces;
using Test.Models;
using Test.Models.ViewModel;
using Test.Repositories;

namespace Test.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _repository;
        private ILogger<Subcategory> _logger;
        public SubcategoryService(ISubcategoryRepository subcategoryRepository,
            ILogger<Subcategory> logger)
        {
            _repository = subcategoryRepository;
            _logger = logger;
        }

        public async Task<List<Subcategory>> CreateSubcategory(CreateSubcategoryModel model,
            int categoryId)
        {
            try
            {
                model.Validation();
                _logger.LogInformation($"Запрос на создание категории: {model.Name}");
                var subcategories = await _repository.GetSubcategories(categoryId);

                var subcategory = subcategories.FirstOrDefault(x => x.Name == model.Name);
                if (subcategory != null)
                {
                    return new List<Subcategory>();
                }
                var newSubategory = new Subcategory
                {
                    Name = model.Name,
                    CategoryId = categoryId
                };
                await _repository.Create(newSubategory);
                _logger.LogInformation($"Создана новая подкатегория: {model.Name}");
                return subcategories;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при создании подкатегории: {ex.Message}");
                return new List<Subcategory>();
            }
        }
    

        public async Task<List<Subcategory>> DeleteSubcategoryy(long id, int categoryId)
        {
            try
            {
                var subcategories = await _repository.GetSubcategories(categoryId);
                var subcategory = subcategories.FirstOrDefault(x => x.Id == id);

                if (subcategory == null)
                {
                    return new List<Subcategory>();
                }
                await _repository.Delete(subcategory);
                return subcategories;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[SubcategoryService.DeleteSubcategoryy]: {ex.Message}");
                return new List<Subcategory>();
            }
        }


        public async Task<List<Subcategory>> EditSubcategory(CreateSubcategoryModel model, 
            int categoryId)
        {
            try
            {
                model.Validation();
                _logger.LogInformation($"Запрос на редактирование подкатегории: {model.Name}");

                var subcategories = await _repository.GetSubcategories(categoryId);
                var subcategory = subcategories.FirstOrDefault(x => x.Id == model.Id) ;

                if (subcategory == null)
                {
                    return new List<Subcategory>();
                }

                subcategory.Name = model.Name;

                await _repository.Update(subcategory);

                _logger.LogInformation($"Подкатегория успешно обновлена: {model.Id}");
                return subcategories;
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[SubcategoryService.EditSubcategory]: {ex.Message}");
                return new List<Subcategory>();
            }
        }
    }
}


