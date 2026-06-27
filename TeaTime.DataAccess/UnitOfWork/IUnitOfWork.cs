using TeaTime.DataAccess.Category;
using TeaTime.DataAccess.Product;
using TeaTime.DataAccess.ShoppingCart;
using TeaTime.DataAccess.Store;

namespace TeaTime.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    
    IProductRepository Product { get; }
    
    IStoreRepository Store { get; }

    IShoppingCartRepository ShoppingCart { get; }

    IApplicationUserRepository ApplicationUser { get; }

    void Save();
}