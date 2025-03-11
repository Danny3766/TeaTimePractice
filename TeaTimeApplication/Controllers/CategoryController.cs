using Microsoft.AspNetCore.Mvc;
using TeaTimeApplication.Data;
using TeaTimeApplication.Models;

namespace TeaTimeApplication.Controllers
{
    /// <summary>
    /// 類別 Controller
    /// </summary>
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="db"></param>
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        /// <summary>
        /// 類別清單 - 首頁
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<CategoryModel> categoryList = _db.Categories.ToList();

            return View(categoryList);
        }
    }
}
