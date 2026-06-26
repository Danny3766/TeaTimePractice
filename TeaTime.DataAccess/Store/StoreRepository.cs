using TeaTime.DataAccess.Data;
using TeaTime.Models;

namespace TeaTime.DataAccess.Store
{
    public class StoreRepository : Repository<StoreModel>, IStoreRepository
    {
        private ApplicationDbContext _db;
        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="db"></param>
        public StoreRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// 更新 實作
        /// </summary>
        /// <param name="store"></param>
        public void Update(StoreModel store)
        {
            _db.Stores.Update(store);
        }
    }
}
