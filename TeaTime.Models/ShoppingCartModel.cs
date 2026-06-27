using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeaTime.Models
{
    /// <summary>
    /// 購物車模型
    /// </summary>
    public class ShoppingCartModel
    {
        /// <summary>
        /// 購物車項目的唯一識別碼，用於在購物車流程中追蹤、更新或移除單筆加入購物車的商品設定。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 加入購物車的商品識別碼，用於將使用者在商品明細頁選擇的商品帶入購物車項目。
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// 加入購物車的商品資料，用於在購物車或結帳流程中顯示商品名稱、價格、尺寸與圖片等資訊。
        /// </summary>
        [ForeignKey("ProductId")]
        [ValidateNever]
        public ProductModel Product { get; set; }

        /// <summary>
        /// 使用者加入購物車的商品數量，用於計算購物車項目小計與後續訂單數量。
        /// </summary>
        [Range(1, 100, ErrorMessage = "請輸入 1 - 100 的數字")]
        public int Count { get; set; }

        /// <summary>
        /// 使用者在商品明細頁選擇的冰量，用於保留飲品客製化設定並帶入購物車與訂單明細。
        /// </summary>
        public string Ice { get; set; }

        /// <summary>
        /// 使用者在商品明細頁選擇的甜度，用於保留飲品客製化設定並帶入購物車與訂單明細。
        /// </summary>
        public string Sweetness { get; set; }

        /// <summary>
        /// 擁有此購物車項目的使用者識別碼，用於將購物車內容綁定到目前登入會員。
        /// </summary>
        public string ApplicationUserId { get; set; }

        /// <summary>
        /// 擁有此購物車項目的使用者資料，用於在購物車、結帳或訂單建立流程中取得會員相關資訊。
        /// </summary>
        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }
    }
}
