using TeaTime.Models;

namespace TeaTime.DataAccess.Order
{
    /// <summary>
    /// 訂單表頭資料存取介面，用於處理訂單主檔的查詢、新增、刪除與更新操作。
    /// </summary>
    public interface IOrderHeaderRepository : IRepository<OrderHeaderModel>
    {
        /// <summary>
        /// 更新訂單表頭資料，用於調整訂單狀態、付款狀態、金流資訊或訂購人聯絡資訊。
        /// </summary>
        /// <param name="orderHeader">要更新的訂單表頭資料。</param>
        void Update(OrderHeaderModel orderHeader);

        /// <summary>
        /// 更新指定訂單的處理狀態，並可依流程需要同步更新付款狀態。
        /// </summary>
        /// <param name="id">要更新狀態的訂單表頭 id。</param>
        /// <param name="orderStatus">訂單目前的處理狀態。</param>
        /// <param name="paymentStatus">選填的付款狀態，用於需要同步調整付款結果的流程。</param>
        void  UpdateStatus(int id, string orderStatus, string? paymentStatus = null);
    }
}
