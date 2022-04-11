namespace StockMeister.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICompanyRepository Company { get; }
        ICategoryRepository Category { get; }
        void Save();

        Task<int> SaveAsync();
    }
}
