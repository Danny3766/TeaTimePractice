using TeaTime.Models;

namespace TeaTime.DataAccess.ShoppingCart
{
    public interface IShoppingCartRepository : IRepository<ShoppingCartModel>
    {
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="shoppingCart"></param>
        void Update(ShoppingCartModel shoppingCart);    
    }
}
