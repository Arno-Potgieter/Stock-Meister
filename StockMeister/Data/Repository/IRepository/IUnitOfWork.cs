namespace StockMeister.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICompanyRepository Company { get; }
        void Save();
    }
}
