using Test.Models.ViewModel;
using Test.Models;

namespace Test.Interfaces
{
    public interface ISubcategoryService
    {
        Task<List<Subcategory>> CreateSubcategory(CreateSubcategoryModel model, int categoryId);
        Task<List<Subcategory>> DeleteSubcategoryy(long id, int categoryId);
        Task<List<Subcategory>> EditSubcategory(CreateSubcategoryModel model, 
            int categoryId);
    }
}
