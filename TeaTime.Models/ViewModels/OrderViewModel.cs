namespace TeaTime.Models.ViewModels
{
    /// <summary>
    /// 訂單管理明細頁使用的 ViewModel，用於在查詢單筆訂單時，同時承載訂單表頭資訊與該訂單底下的商品明細，讓後台可檢視訂購人、金額、狀態與各項訂購內容。
    /// </summary>
    public class OrderViewModel
    {
        /// <summary>
        /// 單筆訂單的表頭資料，用於在訂單明細流程中顯示訂購人聯絡資訊、訂單總金額、訂單狀態與付款狀態。
        /// </summary>
        public OrderHeaderModel OrderHeader { get; set; }

        /// <summary>
        /// 單筆訂單所包含的商品明細集合，用於在訂單明細流程中列出各商品的數量、成交價格、冰量與甜度等訂購內容。
        /// </summary>
        public IEnumerable<OrderDetailModel> OrderDetail { get; set; }
    }
}
