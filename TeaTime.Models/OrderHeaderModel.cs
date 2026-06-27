using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace TeaTime.Models
{
    /// <summary>
    /// 訂單表頭模型，用於在結帳流程中保存單筆訂單的使用者、金額、狀態、付款與聯絡資訊。
    /// </summary>
    public class OrderHeaderModel
    {
        /// <summary>
        /// 訂單表頭的唯一識別碼，用於建立訂單後追蹤該筆訂單及關聯後續訂單明細。
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 建立此訂單的會員識別碼，用於將結帳產生的訂單歸屬到目前登入的使用者。
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        /// 建立此訂單的會員資料，用於訂單建立或查詢時取得會員帳號相關資訊。
        /// </summary>
        [ForeignKey("ApplicationId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        /// <summary>
        /// 訂單建立時間，用於記錄使用者送出結帳並產生訂單表頭的時間點。
        /// </summary>
        public DateTime OrderDate { get; set; }

        /// <summary>
        /// 訂單總金額，用於保存購物車各項商品數量與單價計算後的結帳金額。
        /// </summary>
        public Double OrderTotal { get; set; }

        /// <summary>
        /// 訂單處理狀態，用於表示訂單在成立、確認、製作、完成或取消等流程中的目前進度。
        /// </summary>
        public string? OrderStatus { get; set; }

        /// <summary>
        /// 付款處理狀態，用於表示訂單款項在待付款、已付款或付款失敗等流程中的目前結果。
        /// </summary>
        public string? PaymentStatus { get; set; }

        /// <summary>
        /// 付款完成時間，用於記錄金流確認付款成功後更新訂單付款狀態的時間點。
        /// </summary>
        public DateTime PaymentDate { get; set; }

        /// <summary>
        /// 付款期限，用於記錄使用者需完成付款的截止時間，供逾期判斷或提醒流程使用。
        /// </summary>
        public DateTime PaymentDueDate { get; set; }

        /// <summary>
        /// 金流結帳工作階段識別碼，用於在導向外部付款與付款回傳流程中對應同一筆訂單。
        /// </summary>
        public string? SessionId { get; set; }

        /// <summary>
        /// 訂購人聯絡電話，用於保存結帳時填寫的聯絡方式，供門市確認訂單或通知取餐使用。
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 訂購人地址，用於保存結帳時填寫的地址資訊，供訂單聯絡或配送相關流程使用。
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 訂購人姓名，用於保存結帳時填寫的聯絡姓名，供訂單查詢、門市確認與通知使用。
        /// </summary>
        public string Name { get; set; }
    }
}
