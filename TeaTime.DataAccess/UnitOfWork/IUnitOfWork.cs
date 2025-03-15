using TeaTime.DataAccess.Category;
using TeaTime.DataAccess.Product;

namespace TeaTime.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    
    IProductRepository Product { get; }
    
    void Save();
}