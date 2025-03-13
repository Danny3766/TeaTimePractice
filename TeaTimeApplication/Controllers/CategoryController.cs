﻿using Microsoft.AspNetCore.Mvc;
using TeaTime.DataAccess.Category;
using TeaTime.Models;

namespace TeaTimeApplication.Controllers
{
    /// <summary>
    /// 類別 Controller
    /// </summary>
    public class CategoryController : Controller
    {
        /// <summary>
        /// 注入
        /// </summary>
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="categoryRepository"></param>
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// 類別清單 - 首頁
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            List<CategoryModel> categoryList = _categoryRepository.GetAll().ToList();

            return View(categoryList);
        }

        /// <summary>
        /// 類別清單 - 新增
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// 類別清單 - 資料輸入到 DB
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Create(CategoryModel category)
        {
            if (category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "類別名稱不能和顯示順序一樣");
            }

            // 資料驗證
            if (ModelState.IsValid)
            {
                _categoryRepository.Add(category);
                _categoryRepository.Save();

                // 新增 TempData["success"]
                TempData["success"] = "類別新增成功!!!";
                return RedirectToAction(nameof(Index));
            }
            
            return View();
        }

        /// <summary>
        /// 編輯類別 - 編輯表單
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Edit(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            CategoryModel? categoryFromDb = _categoryRepository.Get(u => u.Id == id);

            if (categoryFromDb is null)
            {
                return NotFound();
            }

            return View(categoryFromDb);    
        }

        /// <summary>
        /// 編輯類別 - 編輯資料輸入到 DB
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Edit(CategoryModel category)
        {
            // 資料驗證
            if (ModelState.IsValid)
            {
                _categoryRepository.Update(category);
                _categoryRepository.Save();

                // 新增 TempData["success"]
                TempData["success"] = "類別編輯成功!!!";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }

        /// <summary>
        /// 刪除類別 - 刪除表單
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id is null || id == 0)
            {
                return NotFound();
            }

            CategoryModel categoryFromDb = _categoryRepository.Get(u => u.Id == id);

            if (categoryFromDb is null)
            {
                return NotFound();
            }

            return View(categoryFromDb);
        }

        /// <summary>
        /// 刪除類別 - 刪除 DB 的資料
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        public IActionResult DeleteByPost(int? id)
        {
            CategoryModel? obj = _categoryRepository.Get(u => u.Id == id);

            if (obj is null)
            {
                return NotFound();
            }

            _categoryRepository.Remove(obj);
            _categoryRepository.Save();

            // 新增 TempData["success"]
            TempData["success"] = "類別刪除成功!!!";
            return RedirectToAction(nameof(Index));
        }
    }
}
