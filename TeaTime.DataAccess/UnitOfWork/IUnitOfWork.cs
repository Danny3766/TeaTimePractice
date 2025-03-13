using TeaTime.DataAccess.Category;

namespace TeaTime.DataAccess;

public interface IUnitOfWork
{
    ICategoryRepository Category { get; }
    
    void Save();
}