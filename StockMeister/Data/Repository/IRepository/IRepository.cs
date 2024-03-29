﻿using System.Linq.Expressions;

namespace StockMeister.Data.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {

        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T entity);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}
