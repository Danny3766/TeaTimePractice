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

        /// <summary>
        /// 依訂單表頭 id 查詢訂單並更新其處理狀態，必要時同步更新付款狀態。
        /// </summary>
        /// <param name="id">要更新狀態的訂單表頭 id。</param>
        /// <param name="orderStatus">要寫入訂單表頭的處理狀態。</param>
        /// <param name="paymentStatus">選填的付款狀態，用於需要同步調整付款結果的流程。</param>
        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var orderFromDb = _db.OrderHeaders.FirstOrDefault(u => u.Id == id);

            if (orderFromDb != null) 
            {
                orderFromDb.OrderStatus = orderStatus;

                if (string.IsNullOrEmpty(paymentStatus)) 
                {
                    orderFromDb.PaymentStatus = paymentStatus;
                }
            }
        }
    }
}
