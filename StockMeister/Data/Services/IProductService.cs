using StockMeister.Models;
using StockMeister.Models.ViewModels;

namespace StockMeister.Data.Services
{
    public interface IProductService
    {
        public Task<int> AddUpdate(ProductVM model);
        public Product GetById(int id);
        public Task<int> DeleteProduct(int id);
    }
}
