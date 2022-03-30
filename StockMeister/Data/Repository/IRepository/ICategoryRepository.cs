using StockMeister.Models;

namespace StockMeister.Data.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void update(Category obj);
    }
}
