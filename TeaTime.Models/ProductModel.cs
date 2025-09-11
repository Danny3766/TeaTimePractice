using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    /// <summary>
    /// 產品的類別 Id
    /// </summary>
    /// <remarks>此欄位作為類別資料表 (CategoryModel) 的外部鍵</remarks>
    public int CategoryId { get; set; }

    /// <summary>
    /// 產品類別
    /// </summary>
    /// <remarks>加上 ForeignKey 的 Attribute 目的，讓 ProductModel 知道會使用 CategoryModel</remarks>
    [ForeignKey("CategoryId")]
    [ValidateNever]
    public CategoryModel Category { get; set; }

    /// <summary>
    /// 產品圖片網址
    /// </summary>
    [ValidateNever]
    public string ProductImageUrl { get; set; }
}