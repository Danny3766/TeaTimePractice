using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TeaTime.DataAccess.UnitOfWork;
using TeaTime.Models;
using TeaTime.Models.ViewModels;
using TeaTime.Utility;

namespace TeaTimeApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 產品 Controller
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        /// <summary>
        /// 注入
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="unitOfWork"></param>
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// 產品清單 - 首頁
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<ProductModel> productList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();

            return View(productList);
        }

        #region 整合新增 & 編輯產品 Upsert

        /// <summary>
        /// 新增 & 編輯產品
        /// </summary>
        /// <param name="id">產品 id</param>
        /// <returns></returns>
        public IActionResult Upsert(int? id) 
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

            // 執行新增
            if (id == null || id == 0)
            {
                return View(productVm);
            }
            else 
            {
                // 執行編輯
                productVm.Product = _unitOfWork.Product.Get(u => u.Id == id);
                return View(productVm);
            }
        }

        /// <summary>
        /// 新增 & 編輯產品 - 資料輸入到 DB
        /// </summary>
        /// <param name="productVm">產品的 Model</param>
        /// <param name="file">檔案</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Upsert(ProductViewModel productVm, IFormFile? file)
        {
            // 資料驗證
            if (ModelState.IsValid)
            {
                // 增加上團圖片的驗證邏輯
                var wwwRootPath = _webHostEnvironment.WebRootPath;
                if (file != null)
                {
                    var fileName = Guid.NewGuid().ToString();
                    Path.GetExtension(file.FileName);
                    var productPath = Path.Combine(wwwRootPath, @"images\product");

                    if (!string.IsNullOrEmpty(productVm.Product.ProductImageUrl)) 
                    {
                        // 有新圖片上傳，刪除舊圖片
                        var oldImagePath = Path.Combine(wwwRootPath, productVm.Product.ProductImageUrl.TrimStart('\\'));
                        if (System.IO.File.Exists(oldImagePath)) 
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(
                        Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    productVm.Product.ProductImageUrl = @"\image\product\" + fileName;
                }

                if (productVm.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVm.Product);
                    
                }
                else 
                {
                    _unitOfWork.Product.Update(productVm.Product);
                }

                _unitOfWork.Save();
                // 新增 TempData["success"]
                TempData["success"] = "產品新增成功!!!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                productVm.CategoryList =
                _unitOfWork.Category.GetAll().Select(u =>
                    new SelectListItem
                    {
                        Text = u.Name,
                        Value = u.Id.ToString()
                    }
                );

                return View(productVm);
            }
        }

        #endregion

        #region API Calls
        
        /// <summary>
        /// 取得全部商品的 API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() 
        {
            List<ProductModel> objProductList = _unitOfWork.Product.GetAll(includeProperties:"Category").ToList();

            return Json(new { data = objProductList });
        }

        /// <summary>
        /// 刪除指定 id 產品
        /// </summary>
        /// <param name="id">產品 id</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int? id) 
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);

            if (productToBeDeleted == null) 
            {
                return Json(new { success = false, message = "刪除失敗!!!"});
            }

            var oldImagaPath = Path.Combine(
                _webHostEnvironment.WebRootPath, productToBeDeleted.ProductImageUrl.TrimStart('\\'));

            if (System.IO.File.Exists(oldImagaPath))
            {
                System.IO.File.Delete(oldImagaPath);
            }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "刪除成功!!!"});
        }

        #endregion

        #region 標記不使用的新增 & 編輯方法

        /// <summary>
        /// 產品清單 - 新增
        /// </summary>
        /// <returns></returns>
        [Obsolete("改使用 Upsert(int? id)")]
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
        [Obsolete("改使用 Upsert(ProductViewModel productVm, IFormFile? file)")]
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
                    new SelectListItem
                    {
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
        [Obsolete("改使用 Upsert(int? id)")]
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
        [Obsolete("改使用 Upsert(ProductViewModel productVm, IFormFile? file)")]
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

        #endregion
    }
}