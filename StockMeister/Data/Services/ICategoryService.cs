using StockMeister.Models;

namespace StockMeister.Data.Services
{
    public interface ICategoryService
    {
        public Task<int> AddUpdate(Category mdoel);
    }
}
