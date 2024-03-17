using Test.Models;
using Test.Models.ViewModel;

namespace Test.Interfaces
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllCategories();
        Task<List<Category>> CreateCategory(CreateCategoryModel model);
        Task<List<Category>> DeleteCategory(long id);
        Task<List<Category>> EditCategory(CreateCategoryModel model);

    }
}
