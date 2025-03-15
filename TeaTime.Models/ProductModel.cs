using System.ComponentModel.DataAnnotations;

namespace TeaTime.Models;

public class ProductModel
{
    /// <summary>
    /// 產品
    /// </summary>
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        [Required]
        public string Size { get; set; }
        
        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }
        
        public string Descreption { get; set; }
    }
}