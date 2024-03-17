using Test.Models;
using Test.Interfaces;
using Test.Data;
using Microsoft.EntityFrameworkCore;

namespace Test.Repositories
{
        public class CategoryRepository : ICategoryRepository
        {
            private readonly AppDbContext _context;

            public CategoryRepository(AppDbContext context)
            {
                _context = context;
            }

        public async Task Create(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Category category)
        {
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Category>> GetAllCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<Category> Update(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
            return category;
        }
    }
}
   

