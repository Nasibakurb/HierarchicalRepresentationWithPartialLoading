using HierarchicalView.Domain.Entity;
using HierarchicalView.Domain.Response;
using HierarchicalView.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalView.Domain.Interfaces.Category
{
    public interface ICategoryService
    {
        Task<IBaseResponse<List<CategoryEntity>>> GetAllCategories();
        Task<IBaseResponse<CategoryEntity>> CreateCategory(CreateCategoryModel model);
        Task<IBaseResponse<CategoryEntity>> DeleteCategory(long id);
        Task<IBaseResponse<CategoryEntity>> EditCategory(CreateCategoryModel model);
    }
}
