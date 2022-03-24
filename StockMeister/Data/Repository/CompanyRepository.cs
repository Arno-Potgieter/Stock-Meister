using StockMeister.Data.Repository.IRepository;
using StockMeister.Models;

namespace StockMeister.Data.Repository
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        private readonly ApplicationDbContext _db;

        public CompanyRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void update(Company obj)
        {
            _db.Companies.Update(obj);
        }
    }
}
