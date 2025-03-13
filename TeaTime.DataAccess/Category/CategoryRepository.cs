using TeaTime.DataAccess.Data;
using TeaTime.Models;

namespace TeaTime.DataAccess.Category
{
    public class CategoryRepository : Repository<CategoryModel>, ICategoryRepository
    {
        private ApplicationDbContext _db;
        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="db"></param>
        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        /// <summary>
        /// 儲存 - 實作
        /// </summary>
        public void Save()
        {
            _db.SaveChanges();
        }

        /// <summary>
        /// 更新 實作
        /// </summary>
        /// <param name="category"></param>
        public void Update(CategoryModel category)
        {
            _db.Categories.Update(category);
        }
    }
}
