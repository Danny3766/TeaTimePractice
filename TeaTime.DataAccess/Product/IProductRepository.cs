using TeaTime.Models;

namespace TeaTime.DataAccess.Product
{
    public interface IProductRepository : IRepository<ProductModel>
    {
        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="product"></param>
        void Update(ProductModel product);
        
        /// <summary>
        /// 儲存
        /// </summary>
        void Save();
    }
}
