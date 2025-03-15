using TeaTime.DataAccess.Data;
using TeaTime.Models;

namespace TeaTime.DataAccess.Product
{
    public class ProductRepository : Repository<ProductModel>, IProductRepository
    {
        private ApplicationDbContext _db;
        /// <summary>
        /// 建構式
        /// </summary>
        /// <param name="db"></param>
        public ProductRepository(ApplicationDbContext db) : base(db)
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
        /// <param name="product"></param>
        public void Update(ProductModel product)
        {
            _db.Products.Update(product);
        }
    }
}
