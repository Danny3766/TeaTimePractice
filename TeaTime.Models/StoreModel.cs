using System.ComponentModel.DataAnnotations;

namespace TeaTime.Models
{
    /// <summary>
    /// 店資料
    /// </summary>
    public class StoreModel
    {
        /// <summary>
        /// 流水號
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 店名稱  
        /// </summary>
        [Required]
        public string Name { get; set; }
        
        /// <summary>
        /// 店地址
        /// </summary>
        public string? Address { get; set; }
        
        /// <summary>
        /// 所在縣市
        /// </summary>
        public string? City { get; set; }
        
        /// <summary>
        /// 聯絡電話
        /// </summary>
        public string? PhoneNumber { get; set; }
        
        /// <summary>
        /// 描述
        /// </summary>
        public string? Description { get; set; }
    }
}
