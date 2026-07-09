namespace TeaTime.Models.ViewModels
{
    /// <summary>
    /// 購物車 ViewModel，用於在購物車與結帳摘要流程中彙整購物車項目與訂單表頭資料。
    /// </summary>
    public class ShoppingCartViewModel
    {
        /// <summary>
        /// 目前登入使用者的購物車項目清單，用於在購物車頁面顯示商品、數量、冰量、甜度與小計資訊。
        /// </summary>
        public IEnumerable<ShoppingCartModel> ShoppingCartList { get; set; }

        //public double OrderTotal { get; set; }

        /// <summary>
        /// 結帳流程中的訂單表頭資料，用於保存訂購人資訊、訂單總金額、訂單狀態與付款相關資料。
        /// </summary>
        public OrderHeaderModel OrderHeader { get; set; }
    }
}
