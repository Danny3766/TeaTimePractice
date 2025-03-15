using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeaTime.DataAccess.UnitOfWork;
using TeaTime.Models;
using TeaTime.Models.ViewModels;

namespace TeaTimeApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 產品 Controller
    /// </summary>
    [Area("Admin")]
    public class ProductController : Controller
    {
        /// <summary>
        /// 注入
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 產品清單 - 首頁
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<ProductModel> productList = _unitOfWork.Product.GetAll().ToList();

            return View(productList);
        }

        /// <summary>
        /// 產品清單 - 新增
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            ProductViewModel productVm = new()
            {
                CategoryList = _unitOfWork.Category.GetAll().Select(u =>
                    new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }),
                Product = new ProductModel()
            };
            
            // 使用 ViewBag 傳遞資料
            //ViewBag.CategoryList = categoryList;
            
            // 使用 ViewData 傳遞資料
            //ViewData["CategoryList"] = categoryList;
            
            return View(productVm);
        }

        /// <summary>
        /// 產品清單 - 資料輸入到 DB
        /// </summary>
        /// <param name="productVm">ProductViewModel 物件</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(ProductViewModel productVm)
        {
            // 資料驗證
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Add(productVm.Product);
                _unitOfWork.Save();

                // 新增 TempData["success"]
                TempData["success"] = "產品新增成功!!!";
                return RedirectToAction(nameof(Index));
            }

            productVm.CategoryList = 
                _unitOfWork.Category.GetAll().Select(u => 
                    new SelectListItem { 
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }
                );

            return View(productVm);
        }

        /// <summary>
        /// 編輯產品 - 編輯表單
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            ProductModel? productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb is null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        /// <summary>
        /// 編輯產品 - 編輯資料輸入到 DB
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(ProductModel product)
        {
            // 資料驗證
            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();

                // 新增 TempData["success"]
                TempData["success"] = "產品編輯成功!!!";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        /// <summary>
        /// 刪除產品 - 刪除表單
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            ProductModel productFromDb = _unitOfWork.Product.Get(u => u.Id == id);

            if (productFromDb is null)
            {
                return NotFound();
            }

            return View(productFromDb);
        }

        /// <summary>
        /// 刪除產品 - 刪除 DB 的資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteByPost(int? id)
        {
            ProductModel? obj = _unitOfWork.Product.Get(u => u.Id == id);

            if (obj is null)
            {
                return NotFound();
            }

            _unitOfWork.Product.Remove(obj);
            _unitOfWork.Save();

            // 新增 TempData["success"]
            TempData["success"] = "產品刪除成功!!!";
            return RedirectToAction(nameof(Index));
        }
    }
}
