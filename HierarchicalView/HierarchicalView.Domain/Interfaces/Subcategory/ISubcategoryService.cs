using HierarchicalView.Domain.Entity;
using HierarchicalView.Domain.Response;
using HierarchicalView.Domain.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalView.Domain.Interfaces.Subcategory
{
    public interface ISubcategoryService
    {
        Task<IBaseResponse<SubcategoryEntity>> CreateSubcategory(CreateSubcategoryModel model, int categoryId);
        Task<IBaseResponse<SubcategoryEntity>> DeleteSubcategoryy(long id, int categoryId);
        Task<IBaseResponse<SubcategoryEntity>> EditSubcategory(CreateSubcategoryModel model,
            int categoryId);
    }
}
