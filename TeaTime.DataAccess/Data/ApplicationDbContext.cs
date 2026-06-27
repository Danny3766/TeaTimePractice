using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TeaTime.Models;

namespace TeaTime.DataAccess.Data
{
    /// <summary>
    /// 資料庫 Context
    /// </summary>
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }
        
        // Categories 資料表
        public DbSet<CategoryModel> Categories { get; set; }
        // Products 資料表
        public DbSet<ProductModel> Products { get; set; }
        // Stores 資料表
        public DbSet<StoreModel> Stores { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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
                    CategoryId = 1,
                    ProductImageUrl = String.Empty
                },
                new ProductModel
                {
                    Id = 2,
                    Name = "鐵觀音",
                    Size = "中杯",
                    Description = "品鐵觀音，享人生味道",
                    Price = 55,
                    CategoryId = 2,
                    ProductImageUrl = String.Empty
                },
                new ProductModel
                {
                    Id = 3,
                    Name = "美式咖啡",
                    Size = "中杯",
                    Description = "用咖啡建構休閒時光",
                    Price = 45,
                    CategoryId = 3,
                    ProductImageUrl = String.Empty
                }
            );

            modelBuilder.Entity<StoreModel>().HasData(
                new StoreModel
                {
                    Id = 1,
                    Name = "台中一中店",
                    Address = "台中市北區三民路三段129號",
                    City = "台中市",
                    PhoneNumber = "04-1234-5678",
                    Description = "鄰近台中一中商圈，學生消暑勝地。"
                },
                new StoreModel
                {
                    Id = 2,
                    Name = "台北大安店",
                    Address = "台北市大安區大安路一段11號",
                    City = "台北市",
                    PhoneNumber = "02-2345-6789",
                    Description = "濃厚的教育文化及熱鬧繁華的商圈，豐富整個氛圍。"
                },
                new StoreModel
                {
                    Id = 3,
                    Name = "台南安平店",
                    Address = "台南市安平區安平路123號",
                    City = "台南市",
                    PhoneNumber = "06-3456-7890",
                    Description = "歷史造就了安平區的獨特風貌，茶香中蘊含了悠遠的歷史。"
                }
            );
        }
    }
}
