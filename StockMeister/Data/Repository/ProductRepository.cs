﻿using StockMeister.Data.Repository.IRepository;
using StockMeister.Models;

namespace StockMeister.Data.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _db;

        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
