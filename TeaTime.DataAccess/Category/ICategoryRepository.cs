using TeaTime.Models;

namespace TeaTime.DataAccess.Category
{
    public interface ICategoryRepository : IRepository<CategoryModel>
    {
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="category"></param>
        void Update(CategoryModel category);
        
        /// <summary>
        /// 儲存
        /// </summary>
        void Save();
    }
}
