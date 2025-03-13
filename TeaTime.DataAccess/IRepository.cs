using System.Linq.Expressions;

namespace TeaTime.DataAccess;

public interface IRepository<T> where T : class
{
    /// <summary>
    /// 取得全部類別
    /// </summary>
    /// <returns></returns>
    /// <remarks>將編輯的功能放在泛型 Repository 之外，只保留新增刪除的部分</remarks>
    IEnumerable<T> GetAll();
    T Get(Expression<Func<T, bool>> filtet);
    
    void Add(T entity);
    
    void Remove(T entity);
    
    void RemoveRange(IEnumerable<T> entity);
}