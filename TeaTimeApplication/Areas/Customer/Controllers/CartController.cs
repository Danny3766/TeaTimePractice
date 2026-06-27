using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaTime.DataAccess.UnitOfWork;
using TeaTime.Models.ViewModels;

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
                u.ApplicationUserId == userId, includeProperties: "Product")
            };

            foreach (var cart in shoppingCartVM.ShoppingCartList)
            {
                shoppingCartVM.OrderTotal += (cart.Product.Price * cart.Count);
            }

            return View(shoppingCartVM);
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
