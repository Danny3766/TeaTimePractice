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
    }
}
