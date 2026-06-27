using TeaTime.DataAccess.Data;
using TeaTime.Models;

namespace TeaTime.DataAccess.ShoppingCart
{
    public class ShoppingCartRepository : Repository<ShoppingCartModel>, IShoppingCartRepository
    {
        private ApplicationDbContext _db;

        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="db"></param>
        public ShoppingCartRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// 更新 實作
        /// </summary>
        /// <param name="shoppingCart"></param>
        public void Update(ShoppingCartModel shoppingCart)
        {
            _db.ShoppingCarts.Update(shoppingCart);
        }
    }
}
