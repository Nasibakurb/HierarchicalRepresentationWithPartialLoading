using HierarchicalView.Domain.Entity;

namespace HierarchicalView.Domain.Interfaces.Category
{
    public interface ICategoryRepository
    {
        Task<List<CategoryEntity>> GetAllCategories();
        Task Create(CategoryEntity category);
        Task Delete(CategoryEntity category);
        Task<CategoryEntity> Update(CategoryEntity category);

    }
}
