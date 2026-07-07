using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
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

        /// <summary>
        /// 接收訂單詳情表單回傳的訂單管理資料。
        /// </summary>
        [BindProperty]
        public OrderViewModel OrderVM { get; set; }

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
        /// <returns>包含指定訂單資料的訂單詳情 View。</returns>
        public IActionResult Details(int orderId) 
        {
            OrderViewModel orderVM = new OrderViewModel
            {
                OrderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == orderId, includeProperties: "ApplicationUser"),
                OrderDetail = _unitOfWork.OrderDetail.GetAll(u => u.OrderHeaderId == orderId, includeProperties: "Product")
            };

            return View(orderVM);
        }

        /// <summary>
        /// 更新訂單訂購人資訊，並在更新完成後返回訂單詳情頁。
        /// </summary>
        /// <returns>重新導向至更新後訂單的詳情頁。</returns>
        public IActionResult UpdateOrderDetail() 
        {
            var orderHeaderFromDb = _unitOfWork.OrderHeader.Get(u => u.Id == OrderVM.OrderHeader.Id);
            orderHeaderFromDb.Name = OrderVM.OrderHeader.Name;
            orderHeaderFromDb.PhoneNumber = OrderVM.OrderHeader.PhoneNumber;
            orderHeaderFromDb.Address = OrderVM.OrderHeader.Address;

            _unitOfWork.OrderHeader.Update(orderHeaderFromDb);
            _unitOfWork.Save();

            TempData["Success"] = "訂購人資訊更新成功!!!";

            return RedirectToAction(nameof(Details), new { orderId = orderHeaderFromDb.Id });
        }



        #region API Calls

        /// <summary>
        /// 取得全部訂單資料，供訂單管理清單 DataTable 載入使用。
        /// </summary>
        /// <returns>包含訂單表頭與會員資料的 JSON 結果。</returns>
        [HttpGet]
        public IActionResult GetAll(string status)
        {
            IEnumerable<OrderHeaderModel> objOrderHeaders;

            if (User.IsInRole(SD.Role_Admin) || User.IsInRole(SD.Role_Employee) || User.IsInRole(SD.Role_Manager))
            {
                objOrderHeaders = _unitOfWork.OrderHeader.GetAll(includeProperties: "ApplicationUser").ToList();
            }
            else 
            {
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
                objOrderHeaders = _unitOfWork.OrderHeader.GetAll(u => 
                u.ApplicationUserId == userId, includeProperties: "ApplicationUser");
            }

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
