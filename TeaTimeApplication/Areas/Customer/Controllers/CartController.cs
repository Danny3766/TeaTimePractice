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

            foreach(var cart in shoppingCartVM.ShoppingCartList)
            {
                shoppingCartVM.OrderTotal += (cart.Product.Price * cart.Count);
            }

            return View(shoppingCartVM);
        }
    }
}
