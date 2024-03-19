using HierarchicalView.Domain.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalView.Domain.Interfaces.Subcategory
{
    public interface ISubcategoryRepository
    {
        Task<List<SubcategoryEntity>> GetSubcategories(int categoryId);
        Task Create(SubcategoryEntity subcategory);
        Task Delete(SubcategoryEntity subcategory);
        Task<SubcategoryEntity> Update(SubcategoryEntity subcategory);
    }
}
