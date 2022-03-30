using StockMeister.Data.Repository.IRepository;
using StockMeister.Models;

namespace StockMeister.Data.Repository
{
    public class StocktakeRepository : Repository<Stocktake>, IStocktakeRepository
    {
        private readonly ApplicationDbContext _db;

        public StocktakeRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Stocktake obj)
        {
            throw new NotImplementedException();
        }

        public void UpdateRange(IEnumerable<Stocktake> obj)
        {
            _db.Stocktakes.UpdateRange(obj);
        }
    }
}
