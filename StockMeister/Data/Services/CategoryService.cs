using Microsoft.AspNetCore.Identity;
using StockMeister.Data.Repository.IRepository;
using StockMeister.Models;
using StockMeister.Models.ViewModels;
using System.Security.Claims;

namespace StockMeister.Data.Services
{
    public class CategoryService : ICategoryService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string loginUserId;

        public CategoryService(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            _userManager = userManager;
        }
        public async Task<int> AddUpdate(CategoryVM model)
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
                var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
                var company = _unitOfWork.Company.GetFirstOrDefault(x => x.Id == user.CompanyId);
                Category category = new Category()
                {
                    CategoryName = model.CategoryName,
                    CompanyId = company.Id,
                };

                _unitOfWork.Category.Add(category);
                await _unitOfWork.SaveAsync();
                return 2;
            }
        }

        public Task<int> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
