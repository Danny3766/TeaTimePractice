using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using TeaTime.DataAccess.Data;

namespace TeaTime.DataAccess;

public class Repository<T> : IRepository<T> where T : class
{
    // 注入
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;
    
    /// <summary>
    /// 建構式
    /// </summary>
    /// <param name="db"></param>
    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
    }
    
    /// <summary>
    /// GetAll 實作
    /// </summary>
    /// <returns></returns>
    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = dbSet;
        return query.ToList();
    }

    /// <summary>
    /// Get 實作
    /// </summary>
    /// <param name="filter"></param>
    /// <returns></returns>
    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = dbSet;
        query = query.Where(filter);
        return query.FirstOrDefault();
    }

    /// <summary>
    /// Add 實作
    /// </summary>
    /// <param name="entity"></param>
    public void Add(T entity)
    {
        dbSet.Add(entity);        
    }

    /// <summary>
    /// Remove 實作
    /// </summary>
    /// <param name="entity"></param>
    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    /// <summary>
    /// RemoveRange 實作
    /// </summary>
    /// <param name="entity"></param>
    public void RemoveRange(IEnumerable<T> entity)
    {
        dbSet.RemoveRange(entity);
    }
}