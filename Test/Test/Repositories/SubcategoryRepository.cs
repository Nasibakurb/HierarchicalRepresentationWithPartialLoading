using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Interfaces;
using Test.Models;

namespace Test.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly AppDbContext _context;

        public SubcategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(Subcategory subcategory)
        {
            await _context.Subcategories.AddAsync(subcategory);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(Subcategory subcategory)
        {
            _context.Remove(subcategory);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Subcategory>> GetSubcategories(int categoryId)
        {
            return await _context.Subcategories
                .Where(s => s.CategoryId == categoryId)
                .ToListAsync();
        }

        public async Task<Subcategory> Update(Subcategory subcategory)
        {
            _context.Update(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }
    }
}
