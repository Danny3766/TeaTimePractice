using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaTime.DataAccess.UnitOfWork;
using TeaTime.Models;

namespace TeaTimeApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 訂單管理清單首頁。
        /// </summary>
        /// <returns>訂單管理清單 View。</returns>
        public IActionResult Index()
        {
            return View();
        }

        #region API Calls

        /// <summary>
        /// 取得全部訂單資料，供訂單管理清單 DataTable 載入使用。
        /// </summary>
        /// <returns>包含訂單表頭與會員資料的 JSON 結果。</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            List<OrderHeaderModel> objOrderHeaders =
                _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();

            return Json(new { data = objOrderHeaders });
        }

        #endregion
    }
}
