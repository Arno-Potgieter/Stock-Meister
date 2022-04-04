using StockMeister.Data.Repository.IRepository;
using StockMeister.Models;

namespace StockMeister.Data.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> AddUpdate(Category model)
        {
            if(model != null && model.Id > 0)
            {
                // Update
                var category = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == model.Id);

                category.CategoryName = model.CategoryName;

                _unitOfWork.Category.Update(category);
                await _unitOfWork.SaveAsync();
                return 1;
            }
            else
            {
                // Create
                Category category = new Category()
                {
                    CategoryName = model.CategoryName,
                };

                _unitOfWork.Category.Add(category);
                await _unitOfWork.SaveAsync();
                return 2;
            }
        }
    }
}
