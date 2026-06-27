using TeaTime.DataAccess.Data;
using TeaTime.Models;

namespace TeaTime.DataAccess.Order
{
    /// <summary>
    /// 訂單表頭資料存取實作，負責透過資料庫內容管理訂單主檔資料。
    /// </summary>
    public class OrderHeaderRepository : Repository<OrderHeaderModel>, IOrderHeaderRepository
    {
        private ApplicationDbContext _db;

        /// <summary>
        /// 初始化訂單表頭 Repository，並注入共用的資料庫內容供訂單主檔資料操作使用。
        /// </summary>
        /// <param name="db">應用程式資料庫內容。</param>
        public OrderHeaderRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// 更新訂單表頭資料，將指定的訂單主檔標記為更新狀態，供後續儲存流程寫入資料庫。
        /// </summary>
        /// <param name="orderHeader">要更新的訂單表頭資料。</param>
        public void Update(OrderHeaderModel orderHeader)
        {
            _db.OrderHeaders.Update(orderHeader);
        }
    }
}
