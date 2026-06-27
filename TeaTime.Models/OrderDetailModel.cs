using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeaTime.Models
{
    /// <summary>
    /// 訂單明細模型，用於保存訂單表頭底下的單筆商品、數量、成交價格與飲品客製化設定。
    /// </summary>
    public class OrderDetailModel
    {
        /// <summary>
        /// 訂單明細的唯一識別碼，用於追蹤訂單中單筆商品明細資料。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 所屬訂單表頭識別碼，用於將此筆商品明細關聯到結帳時建立的同一筆訂單。
        /// </summary>
        public int OrderHeaderId { get; set; }

        /// <summary>
        /// 所屬訂單表頭資料，用於查詢訂單明細時取得該筆訂單的會員、金額、狀態與付款資訊。
        /// </summary>
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeaderModel OrderHeader { get; set; }

        /// <summary>
        /// 訂購商品識別碼，用於保存從購物車轉入訂單明細的商品來源。
        /// </summary>
        [Required]
        public int ProductId { get; set; }

        /// <summary>
        /// 訂購商品資料，用於在訂單查詢或明細顯示流程中取得商品名稱、尺寸、圖片與分類等資訊。
        /// </summary>
        [ForeignKey("ProductId")]
        [ValidateNever]
        public ProductModel Product { get; set; }

        /// <summary>
        /// 訂購數量，用於保存使用者結帳時購物車中此商品設定的數量並計算明細小計。
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// 訂購單價，用於保存結帳當下商品的成交價格，避免後續商品價格異動影響既有訂單金額。
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// 訂購冰量，用於保存使用者在商品明細或購物車流程中選擇的飲品冰量設定。
        /// </summary>
        public string Ice { get; set; }

        /// <summary>
        /// 訂購甜度，用於保存使用者在商品明細或購物車流程中選擇的飲品甜度設定。
        /// </summary>
        public string Sweetness { get; set; }
    }
}
