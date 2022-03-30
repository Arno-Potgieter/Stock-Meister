using StockMeister.Models;

namespace StockMeister.Data.Repository.IRepository
{
    public interface IProductRepository : IRepository<Product>,
    {
        void Update(Product obj);
    }
}
