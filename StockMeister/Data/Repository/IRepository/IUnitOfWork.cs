namespace StockMeister.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICompanyRepository Company { get; }
        ICategoryRepository Category { get; }

        IProductRepository Product { get; }
        void Save();

        Task<int> SaveAsync();
    }
}
