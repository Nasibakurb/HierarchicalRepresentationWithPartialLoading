using HierarchicalView.Domain.Entity;
using HierarchicalView.Domain.Interfaces.Subcategory;
using HierarchicalView.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HierarchicalView.Infrastructure.Repositories
{
    public class SubcategoryRepository : ISubcategoryRepository
    {
        private readonly AppDbContext _context;

        public SubcategoryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Create(SubcategoryEntity subcategory)
        {
            await _context.Subcategories.AddAsync(subcategory);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(SubcategoryEntity subcategory)
        {
            _context.Remove(subcategory);
            await _context.SaveChangesAsync();
        }

        public async Task<List<SubcategoryEntity>> GetSubcategories(int categoryId)
        {
            return await _context.Subcategories
                .Where(s => s.CategoryEntityId == categoryId)
                .ToListAsync();
        }

        public async Task<SubcategoryEntity> Update(SubcategoryEntity subcategory)
        {
            _context.Update(subcategory);
            await _context.SaveChangesAsync();
            return subcategory;
        }
    }
}
