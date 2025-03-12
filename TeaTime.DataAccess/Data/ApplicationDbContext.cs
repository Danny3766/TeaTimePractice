using Microsoft.EntityFrameworkCore;
using TeaTimeApplication.Models;

namespace TeaTimeApplication.Data
{
    /// <summary>
    /// 資料庫 Context
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public DbSet<CategoryModel> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { Id = 1, Name = "茶飲", DisplayOrder = 1 },
                new CategoryModel { Id = 2, Name = "水果茶", DisplayOrder = 2 },
                new CategoryModel { Id = 3, Name = "咖啡", DisplayOrder = 3 }
                );
        }
    }
}
