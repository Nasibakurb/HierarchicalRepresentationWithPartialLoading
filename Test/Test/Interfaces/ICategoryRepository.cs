using System.Collections.Generic;
using Test.Models;

namespace Test.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetAllCategories();
        Task Create(Category category);
        Task Delete(Category category);
        Task<Category> Update(Category category);
    
    }
}
