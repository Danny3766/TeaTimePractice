using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaTime.DataAccess.UnitOfWork;
using TeaTime.Models;
using TeaTime.Models.ViewModels;
using TeaTime.Utility;

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

        /// <summary>
        /// 顯示指定訂單的管理詳情，包含訂單表頭、會員資料、訂單明細與商品資料。
        /// </summary>
        /// <param name="orderId">要查詢的訂單編號。</param>
        /// <param name="id">前端訂單清單連結傳入的訂單編號。</param>
        /// <returns>包含指定訂單資料的訂單詳情 View；查無訂單時回傳 404。</returns>
        public IActionResult Details(int orderId, int id) 
        {
            int targetOrderId = orderId == 0 ? id : orderId;

            OrderViewModel orderVM = new OrderViewModel
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == targetOrderId, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == targetOrderId, includeProperties: "Product")
            };

            if (orderVM.OrderHeader == null)
            {
                return NotFound();
            }

            return View(orderVM);
        }

        #region API Calls

        /// <summary>
        /// 取得全部訂單資料，供訂單管理清單 DataTable 載入使用。
        /// </summary>
        /// <returns>包含訂單表頭與會員資料的 JSON 結果。</returns>
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeaderModel> objOrderHeaders =
                _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();

            switch(status)
            {
                case "Pending":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusPending);
                    break;

                case "Processing":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusInProcess);
                    break;

                case "Ready":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusReady);
                    break;

                case "Completed":
                    objOrderHeaders = objOrderHeaders.Where(u => u.OrderStatus == SD.StatusCompleted);
                    break;

                default:
                    break;
            }

            return Json(new { data = objOrderHeaders });
        }

        #endregion
    }
}
