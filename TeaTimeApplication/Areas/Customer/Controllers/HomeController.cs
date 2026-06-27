using System.Diagnostics;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeaTime.DataAccess.UnitOfWork;
using TeaTime.Models;

namespace TeaTimeApplication.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<ProductModel> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");

            return View(productList);
        }

        public IActionResult Details(int productId) 
        {
            ShoppingCartModel cart = new()
            {
                Product = _unitOfWork.Product
                            .Get(u => u.Id == productId, includeProperties: "Category"),
                Count = 1,

                ProductId = productId
            };

            return View(cart);
        }
        
        /// <summary>
        /// 將選取的商品與數量加入已登入使用者的購物車。
        /// </summary>
        /// <param name="shoppingCart">從商品詳細資料表單送出的購物車項目。</param>
        /// <returns>儲存購物車項目後重新導向至商品索引頁。</returns>
        [HttpPost]
        [Authorize]
        public IActionResult Details(ShoppingCartModel shoppingCart) 
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;

            var userId = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value;
            shoppingCart.ApplicationUserId = userId;

            ShoppingCartModel cartFromDb = _unitOfWork.ShoppingCart.Get(u =>
            u.ApplicationUser.Id == userId &&
            u.ProductId == shoppingCart.ProductId &&
            u.Ice == shoppingCart.Ice &&
            u.Sweetness == shoppingCart.Sweetness);

            if (cartFromDb != null) 
            {
                // 購物車已建立
                cartFromDb.Count += shoppingCart.Count;
                _unitOfWork.ShoppingCart.Update(cartFromDb);
            }
            else 
            {
                // 新增購物車
                _unitOfWork.ShoppingCart.Add(shoppingCart);
            }

            TempData["success"] = "商品已成功加入購物車！";
            _unitOfWork.Save();

            return RedirectToAction(nameof(Index));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
