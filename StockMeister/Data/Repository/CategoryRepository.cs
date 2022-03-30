﻿using StockMeister.Data.Repository.IRepository;
using StockMeister.Models;

namespace StockMeister.Data.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _db;

        public CategoryRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
