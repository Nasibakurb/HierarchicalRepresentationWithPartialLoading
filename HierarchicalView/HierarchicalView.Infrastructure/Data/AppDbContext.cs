using HierarchicalView.Domain.Entity;
using Microsoft.EntityFrameworkCore;
 

namespace HierarchicalView.Infrastructure.Data
{ 
    public class AppDbContext : DbContext // Наслед. от DbContext EF
    {
        public AppDbContext(DbContextOptions options) : base(options) // Конструктор
        {
            Database.EnsureCreated(); // При обращ. к дб созадаться опред. дб
        }

        public DbSet<CategoryEntity> Categories { get; set; } // Созд. сущности
        public DbSet<SubcategoryEntity> Subcategories { get; set; } // Созд. сущности
    }
}
