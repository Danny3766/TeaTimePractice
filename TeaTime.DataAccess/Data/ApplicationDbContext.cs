using Microsoft.EntityFrameworkCore;
using TeaTime.Models;

namespace TeaTime.DataAccess.Data
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
        
        // Categories 資料表
        public DbSet<CategoryModel> Categories { get; set; }
        // Products 資料表
        public DbSet<ProductModel> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CategoryModel>().HasData(
                new CategoryModel { Id = 1, Name = "果汁", DisplayOrder = 1 },
                new CategoryModel { Id = 2, Name = "茶", DisplayOrder = 2 },
                new CategoryModel { Id = 3, Name = "咖啡", DisplayOrder = 3 }
            );

            modelBuilder.Entity<ProductModel>().HasData(
                new ProductModel
                {
                    Id = 1,
                    Name = "特調水果汁",
                    Size = "大杯",
                    Description = "天然果飲，果香迷人多變，好喝",
                    Price = 60,
                    CategoryId = 1
                },
                new ProductModel
                {
                    Id = 2,
                    Name = "鐵觀音",
                    Size = "中杯",
                    Description = "品鐵觀音，享人生味道",
                    Price = 55,
                    CategoryId = 2
                },
                new ProductModel
                {
                    Id = 3,
                    Name = "美式咖啡",
                    Size = "中杯",
                    Description = "用咖啡建構休閒時光",
                    Price = 45,
                    CategoryId = 3
                }
            );
        }
    }
}
