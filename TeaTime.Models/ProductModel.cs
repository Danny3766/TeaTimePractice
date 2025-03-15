using System.ComponentModel.DataAnnotations;

namespace TeaTime.Models;

/// <summary>
/// 產品 Model
/// </summary>
public class ProductModel
{
    /// <summary>
    /// 產品 Id
    /// </summary>
    public int Id { get; set; }
    
    /// <summary>
    /// 產品名稱
    /// </summary>
    [Required]
    public string Name { get; set; }
    
    /// <summary>
    /// 產品 Size
    /// </summary>
    [Required]
    public string Size { get; set; }
    
    /// <summary>
    /// 產品價格
    /// </summary>
    [Required]
    [Range(1, 10000)]
    public double Price { get; set; }
    
    /// <summary>
    /// 產品描述
    /// </summary>
    public string Description { get; set; }
}