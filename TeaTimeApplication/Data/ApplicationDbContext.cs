using Microsoft.EntityFrameworkCore;

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
    }
}
