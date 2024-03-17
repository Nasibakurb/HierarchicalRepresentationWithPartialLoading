using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Test.Models;

namespace Test.Data
{
    public class AppDbContext : DbContext // Наслед. от DbContext EF
    {
        public AppDbContext(DbContextOptions options) : base(options) // Конструктор
        {
            Database.EnsureCreated(); // При обращ. к дб созадаться опред. дб
        }

        public DbSet<Category> Categories { get; set; } // Созд. сущности
        public DbSet<Subcategory> Subcategories { get; set; } // Созд. сущности
    }
}
