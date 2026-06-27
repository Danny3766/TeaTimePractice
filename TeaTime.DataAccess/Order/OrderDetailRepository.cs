using TeaTime.DataAccess.Data;
using TeaTime.Models;

namespace TeaTime.DataAccess.Order
{
    /// <summary>
    /// 訂單明細資料存取實作，負責透過資料庫內容管理訂單中各商品明細資料。
    /// </summary>
    public class OrderDetailRepository : Repository<OrderDetailModel>, IOrderDetailRepository
    {
        private ApplicationDbContext _db;

        /// <summary>
        /// 初始化訂單明細 Repository，並注入共用的資料庫內容供訂單明細資料操作使用。
        /// </summary>
        /// <param name="db">應用程式資料庫內容。</param>
        public OrderDetailRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// 更新訂單明細資料，將指定的訂單明細標記為更新狀態，供後續儲存流程寫入資料庫。
        /// </summary>
        /// <param name="orderDetail">要更新的訂單明細資料。</param>
        public void Update(OrderDetailModel orderDetail)
        {
            // 更新訂單明細資料的邏輯
            _db.OrderDetails.Update(orderDetail);
        }
    }
}
