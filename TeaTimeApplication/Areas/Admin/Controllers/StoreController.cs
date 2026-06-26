using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaTime.DataAccess.UnitOfWork;
using TeaTime.Models;
using TeaTime.Utility;

namespace TeaTimeApplication.Areas.Admin.Controllers
{
    /// <summary>
    /// 產品 Controller
    /// </summary>
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class StoreController : Controller
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
        public StoreController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
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
            List<StoreModel> storeList = _unitOfWork.Store.GetAll().ToList();

            return View(storeList);
        }

        #region 整合新增 & 編輯店鋪 Upsert

        /// <summary>
        /// 新增 & 編輯店鋪
        /// </summary>
        /// <param name="id">店 id</param>
        /// <returns></returns>
        public IActionResult Upsert(int? id) 
        {
            // 執行新增
            if (id == null || id == 0)
            {
                return View(new StoreModel());
            }
            else 
            {
                // 執行編輯
                var store = _unitOfWork.Store.Get(u => u.Id == id);
                return View();
            }
        }

        /// <summary>
        /// 新增 & 編輯店鋪 - 資料輸入到 DB
        /// </summary>
        /// <param name="store">店鋪的 Model</param>
        /// <param name="file">檔案</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Upsert(StoreModel store, IFormFile? file)
        {
            // 資料驗證
            if (ModelState.IsValid)
            {
                if (store.Id == 0) 
                {
                    _unitOfWork.Store.Add(store);
                }
                else 
                {
                    _unitOfWork.Store.Update(store);
                }

                _unitOfWork.Save();
                // 新增 TempData["success"]
                TempData["success"] = "店鋪新增成功!!!";
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(store);
            }
        }

        #endregion

        #region API Calls
        
        /// <summary>
        /// 取得全部店鋪的 API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll() 
        {
            List<StoreModel> objStoreList = _unitOfWork.Store.GetAll().ToList();

            return Json(new { data = objStoreList });
        }

        /// <summary>
        /// 刪除指定 id 店鋪
        /// </summary>
        /// <param name="id">店鋪 id</param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(int? id) 
        {
            var storeToBeDeleted = _unitOfWork.Store.Get(u => u.Id == id);

            if (storeToBeDeleted == null) 
            {
                return Json(new { success = false, message = "刪除失敗!!!"});
            }
            
            _unitOfWork.Store.Remove(storeToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = true, message = "刪除成功!!!"});
        }

        #endregion
    }
}