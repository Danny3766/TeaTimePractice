using TeaTime.DataAccess.Category;
using TeaTime.DataAccess.Product;
using TeaTime.DataAccess.Store;

namespace TeaTime.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    
    IProductRepository Product { get; }
    
    IStoreRepository Store { get; }

    void Save();
}