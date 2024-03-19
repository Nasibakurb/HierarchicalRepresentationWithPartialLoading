using HierarchicalView.Domain.Entity;
using HierarchicalView.Domain.Enum;
using HierarchicalView.Domain.Interfaces.Subcategory;
using HierarchicalView.Domain.Response;
using HierarchicalView.Domain.ViewModel;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalView.Infrastructure.Services
{
    public class SubcategoryService : ISubcategoryService
    {
        private readonly ISubcategoryRepository _repository;
        private ILogger<SubcategoryEntity> _logger;
        public SubcategoryService(ISubcategoryRepository subcategoryRepository,
            ILogger<SubcategoryEntity> logger)
        {
            _repository = subcategoryRepository;
            _logger = logger;
        }

        public async Task<IBaseResponse<SubcategoryEntity>> CreateSubcategory(CreateSubcategoryModel model, int categoryId)
        {
            try
            {
                model.Validation();
                _logger.LogInformation($"Запрос на создание категории: {model.Name}");
                var subcategories = await _repository.GetSubcategories(categoryId);

                var subcategory = subcategories.FirstOrDefault(x => x.Name == model.Name);
                if (subcategory != null)
                {
                    return new BaseResponse<SubcategoryEntity>() // Создается новый объект
                    {
                        Description = "Задача с таким названием уже есть", // Описание ошибки
                        StatusCode = StatusCode.EntityIsHasAlready // Код состоян. ошибки 
                    };
                }
                var newSubategory = new SubcategoryEntity
                {
                    Name = model.Name,
                    CategoryEntityId = categoryId
                };
                await _repository.Create(newSubategory);

                _logger.LogInformation($"Создана новая подкатегория: {model.Name}");
                return new BaseResponse<SubcategoryEntity>()
                { // Возвр. объект с информацией 
                    Description = "Данная задача создалась",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ошибка при создании подкатегории: {ex.Message}");
                return new BaseResponse<SubcategoryEntity>()
                {
                    Description = $"Ошибка: {ex.Message}",
                    StatusCode = StatusCode.EntityIsHasAlready
                };
            }
        }

        public async Task<IBaseResponse<SubcategoryEntity>> DeleteSubcategoryy(long id, int categoryId)
        {
            try
            {
                var subcategories = await _repository.GetSubcategories(categoryId);
                var subcategory = subcategories.FirstOrDefault(x => x.Id == id);

                if (subcategory == null)
                {
                    return new BaseResponse<SubcategoryEntity>()
                    {
                        Description = $"Задача не найдена",
                        StatusCode = StatusCode.EntitykNotFound
                    };
                }
                await _repository.Delete(subcategory);
                return new BaseResponse<SubcategoryEntity>()
                {
                    Description = $"Задача удалена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[SubcategoryService.DeleteSubcategoryy]: {ex.Message}");
                return new BaseResponse<SubcategoryEntity>()
                {
                    Description = $"Ошибка: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }

        public async Task<IBaseResponse<SubcategoryEntity>> EditSubcategory(CreateSubcategoryModel model, int categoryId)
        {
            try
            {
                model.Validation();
                _logger.LogInformation($"Запрос на редактирование подкатегории: {model.Name}");

                var subcategories = await _repository.GetSubcategories(categoryId);
                var subcategory = subcategories.FirstOrDefault(x => x.Id == model.Id);

                if (subcategory == null)
                {
                    return new BaseResponse<SubcategoryEntity>()
                    {
                        Description = "Задача не найдена",
                        StatusCode = StatusCode.EntitykNotFound
                    };
                }

                subcategory.Name = model.Name;

                await _repository.Update(subcategory);

                _logger.LogInformation($"Подкатегория успешно обновлена: {model.Id}");
                return new BaseResponse<SubcategoryEntity>()
                {
                    Description = "Задача успешно обновлена",
                    StatusCode = StatusCode.Ok
                };
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"[SubcategoryService.EditSubcategory]: {ex.Message}");
                return new BaseResponse<SubcategoryEntity>()
                {
                    Description = $"Ошибка: {ex.Message}",
                    StatusCode = StatusCode.InternalServerError
                };
            }
        }
    }
}
