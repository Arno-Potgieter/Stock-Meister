using StockMeister.Models;
using StockMeister.Models.ViewModels;

namespace StockMeister.Data.Services
{
    public interface ICategoryService
    {
        public Task<int> AddUpdate(CategoryVM model);
        public Category GetById(int id);
        public Task<int> DeleteCategory(int id);
    }
}
