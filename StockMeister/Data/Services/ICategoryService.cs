using StockMeister.Models;
using StockMeister.Models.ViewModels;

namespace StockMeister.Data.Services
{
    public interface ICategoryService
    {
        public Task<int> AddUpdate(CategoryVM model);
        public Task<int> GetAll();
    }
}
