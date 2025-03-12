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
        [DisplayName("類別名稱")]
        public string Name { get; set; }

        [DisplayName("顯示順序")]
        public int DisplayOrder { get; set; }
    }
}
