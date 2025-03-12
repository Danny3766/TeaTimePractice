using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TeaTimeApplication.Models
{
    /// <summary>
    /// 類別 Model
    /// </summary>
    public class CategoryModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)] // 輸入最多 30 個字元
        [DisplayName("類別名稱")]
        public string Name { get; set; }

        [Range(1,100)] // 輸入的值須在 1-100 之間
        [DisplayName("顯示順序")]
        public int DisplayOrder { get; set; }
    }
}
