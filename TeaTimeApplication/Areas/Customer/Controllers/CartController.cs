using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TeaTime.DataAccess.UnitOfWork;
using TeaTime.Models;
using TeaTime.Models.ViewModels;
using TeaTime.Utility;

namespace TeaTimeApplication.Areas.Customer.Controllers
{
    [Area("Customer")]
    [Authorize]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCartViewModel shoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var claimsIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            shoppingCartVM = new ShoppingCartViewModel
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u =>
                u.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new()
            };

            foreach (var cart in shoppingCartVM.ShoppingCartList)
            {
                shoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
            }

            return View(shoppingCartVM);
        }

        /// <summary>
        /// 顯示購物車結帳摘要頁面。
        /// </summary>
        /// <returns>購物車結帳摘要 View。</returns>
        public IActionResult Summary()
        {
            var claimsIdentity = (System.Security.Claims.ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(
                System.Security.Claims.ClaimTypes.NameIdentifier).Value;

            var shoppingCartVM = new ShoppingCartViewModel
            {
                ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u =>
                u.ApplicationUserId == userId, includeProperties: "Product"),
                OrderHeader = new()
            };

            shoppingCartVM.OrderHeader.ApplicationUser = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            shoppingCartVM.OrderHeader.Name = shoppingCartVM.OrderHeader.ApplicationUser.Name;
            shoppingCartVM.OrderHeader.PhoneNumber = shoppingCartVM.OrderHeader.ApplicationUser.PhoneNumber;
            shoppingCartVM.OrderHeader.Address = shoppingCartVM.OrderHeader.ApplicationUser.Address;

            foreach (var cart in shoppingCartVM.ShoppingCartList)
            {
                shoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
            }

            return View(shoppingCartVM);
        }

        /// <summary>
        /// 處理購物車結帳摘要送出流程，建立訂單表頭與各項訂單明細。
        /// </summary>
        /// <param name="shoppingCartVM">包含結帳聯絡資訊與訂單表頭資料的購物車檢視模型。</param>
        /// <returns>訂單建立完成後重新導向至訂單確認頁面。</returns>
        [HttpPost]
        [ActionName("Summary")]
        public IActionResult SummaryPOST(ShoppingCartViewModel shoppingCartVM)
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;

            shoppingCartVM.ShoppingCartList = _unitOfWork.ShoppingCart.GetAll(u =>
                u.ApplicationUserId == userId, includeProperties: "Product");
            shoppingCartVM.OrderHeader.OrderDate = DateTime.Now;
            // 訂單日期為今天日期
            shoppingCartVM.OrderHeader.ApplicationId = userId;
            ApplicationUser application = _unitOfWork.ApplicationUser.Get(u => u.Id == userId);
            
            // 計算訂單總金額
            foreach (var cart in shoppingCartVM.ShoppingCartList)
            {
                shoppingCartVM.OrderHeader.OrderTotal += (cart.Product.Price * cart.Count);
            }

            _unitOfWork.OrderHeader.Add(shoppingCartVM.OrderHeader);
            _unitOfWork.Save();

            // 創建訂單細節資訊
            foreach (var cart in shoppingCartVM.ShoppingCartList) 
            {
                var orderDetailModel = new OrderDetailModel()
                {
                    ProductId = cart.ProductId,
                    OrderHeaderId = shoppingCartVM.OrderHeader.Id,
                    Ice = cart.Ice,
                    Sweetness = cart.Sweetness,
                    Price = cart.Product.Price,
                    Count = cart.Count
                };

                _unitOfWork.OrderDetail.Add(orderDetailModel);
                _unitOfWork.Save();
            }

            // 在這邊重導向到一個動作，並將訂單編號傳遞下去
            return RedirectToAction(nameof(OrderConfirmation), new { id = shoppingCartVM.OrderHeader.Id });
        }

        /// <summary>
        /// 顯示訂單建立完成後的確認頁面。
        /// </summary>
        /// <param name="id">已建立訂單的訂單表頭 id。</param>
        /// <returns>顯示訂單編號的訂單確認 View。</returns>
        public IActionResult OrderConfirmation(int id)
        {
            // 送出送等待店家確認訂單
            var orderHeader = _unitOfWork.OrderHeader.Get(u => u.Id == id, includeProperties: "ApplicationUser");
            _unitOfWork.OrderHeader.UpdateStatus(id, SD.StatusPending);
            // 送出訂單後將購物車內的商品刪除
            List<ShoppingCartModel> shoppingCarts = _unitOfWork.ShoppingCart.GetAll(u =>
            u.ApplicationUserId == orderHeader.ApplicationId).ToList();

            _unitOfWork.ShoppingCart.RemoveRange(shoppingCarts);
            _unitOfWork.Save();

            return View(id);
        }

        /// <summary>
        /// 增加指定購物車項目的商品數量。
        /// </summary>
        /// <param name="cartId">購物車項目 id。</param>
        /// <returns>更新數量後重新導向至購物車頁面。</returns>
        public IActionResult Plus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            cartFromDb.Count += 1;

            _unitOfWork.ShoppingCart.Update(cartFromDb);
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 減少指定購物車項目的商品數量，數量為 1 時移除該項目。
        /// </summary>
        /// <param name="cartId">購物車項目 id。</param>
        /// <returns>更新數量或移除項目後重新導向至購物車頁面。</returns>
        public IActionResult Minus(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);

            if (cartFromDb.Count <= 1)
            {
                // 從購物車中移除該商品
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
            }
            else
            {
                cartFromDb.Count -= 1;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }

            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// 移除指定的購物車項目。
        /// </summary>
        /// <param name="cartId">購物車項目 id。</param>
        /// <returns>移除項目後重新導向至購物車頁面。</returns>
        public IActionResult Remove(int cartId)
        {
            var cartFromDb = _unitOfWork.ShoppingCart.Get(u => u.Id == cartId);
            
            if (cartFromDb != null)
            {
                _unitOfWork.ShoppingCart.Remove(cartFromDb);
                _unitOfWork.Save();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}

