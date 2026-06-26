using TeaTime.Models;

namespace TeaTime.DataAccess.Store
{
    public interface IStoreRepository : IRepository<StoreModel>
    {
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="store"></param>
        void Update(StoreModel store);
    }
}
