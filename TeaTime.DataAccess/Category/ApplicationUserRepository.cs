using TeaTime.DataAccess.Data;
using TeaTime.Models;

namespace TeaTime.DataAccess.Category
{
    public class ApplicationUserRepository : Repository<ApplicationUser>, IApplicationUserRepository
    {
        private ApplicationDbContext _db;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="db"></param>
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
