using StockMeister.Models;

namespace StockMeister.Data.Repository.IRepository
{
    public interface IStocktakeRepository : IRepository<Stocktake>
    {
        void Update(Stocktake obj);

        void UpdateRange(IEnumerable<Stocktake> obj);
    }
}
