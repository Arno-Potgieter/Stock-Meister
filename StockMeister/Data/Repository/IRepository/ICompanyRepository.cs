using StockMeister.Models;

namespace StockMeister.Data.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        void update(Company obj);
    }
}
