using TeaTime.DataAccess.Category;
using TeaTime.DataAccess.Data;
using TeaTime.DataAccess.Product;

namespace TeaTime.DataAccess.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _db;
    public ICategoryRepository Category { get; private set; }
    public IProductRepository Product { get; private set; }

    /// <summary>
    /// 建構式
    /// </summary>
    /// <param name="db"></param>
    public UnitOfWork(ApplicationDbContext db)
    {
        _db = db;
        Category = new CategoryRepository(_db);
        Product = new ProductRepository(_db);
    }
    
    /// <summary>
    /// Save 資料的實作
    /// </summary>
    public void Save()
    {
        _db.SaveChanges();
    }
}