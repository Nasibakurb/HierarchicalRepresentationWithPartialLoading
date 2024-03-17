using Test.Models;

namespace Test.Interfaces
{
    public interface ISubcategoryRepository
    {
        Task<List<Subcategory>> GetSubcategories(int categoryId);
        Task Create(Subcategory subcategory);
        Task Delete(Subcategory subcategory);
        Task<Subcategory> Update(Subcategory subcategory);
    }
}
