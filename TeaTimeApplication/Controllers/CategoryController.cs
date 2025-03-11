using Microsoft.AspNetCore.Mvc;

namespace TeaTimeApplication.Controllers
{
    /// <summary>
    /// 類別 Controller
    /// </summary>
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
