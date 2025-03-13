using TeaTime.DataAccess.Category;

namespace TeaTime.DataAccess.UnitOfWork;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    
    void Save();
}