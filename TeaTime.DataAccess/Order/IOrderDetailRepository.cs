using TeaTime.Models;

namespace TeaTime.DataAccess.Order
{
    /// <summary>
    /// 訂單明細資料存取介面，用於處理訂單中各商品明細的查詢、新增、刪除與更新操作。
    /// </summary>
    public interface IOrderDetailRepository : IRepository<OrderDetailModel>
    {
        /// <summary>
        /// 更新訂單明細資料，用於調整訂單中單筆商品的數量、價格或飲品客製化設定。
        /// </summary>
        /// <param name="orderDetail">要更新的訂單明細資料。</param>
        void Update(OrderDetailModel orderDetail);
    }
}
