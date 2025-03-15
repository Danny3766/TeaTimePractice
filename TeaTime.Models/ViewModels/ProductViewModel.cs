using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TeaTime.Models.ViewModels;

public class ProductViewModel
{
    /// <summary>
    /// 產品
    /// </summary>
    public ProductModel Product { get; set; }

    /// <summary>
    /// 類別清單
    /// </summary>
    [ValidateNever]
    public IEnumerable<SelectListItem> CategoryList { get; set; }
}