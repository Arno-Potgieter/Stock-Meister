using StockMeister.Data.Repository.IRepository;

namespace StockMeister.Data.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Company = new CompanyRepository(_db);
            Category = new CategoryRepository(_db);
            Product = new ProductRepository(_db);
        }

        public ICompanyRepository Company { get; private set; }

        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task<int> SaveAsync()
        {
           return await _db.SaveChangesAsync();
        }
    }
}
