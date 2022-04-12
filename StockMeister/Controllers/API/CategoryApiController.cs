using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMeister.Data.Repository.IRepository;
using StockMeister.Data.Services;
using StockMeister.Models;
using StockMeister.Models.ViewModels;
using System.Security.Claims;

namespace StockMeister.Controllers.API
{
    [Route("Controllers/api/Category")]
    [ApiController]
    public class CategoryApiController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string loginUserId;
        private readonly string role;
        public CategoryApiController(ICategoryService categoryService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
        }

        [HttpPost]
        [Route("SaveCategoryData")]
        public IActionResult SaveCategoryData(CategoryVM data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _categoryService.AddUpdate(data).Result;
                if(commonResponse.status == 1)
                {
                    // Update
                    commonResponse.message = Data.Static_Data.CategoryMessages.categoryUpdated;
                }
                if(commonResponse.status == 2)
                {
                    // Create
                    commonResponse.message = Data.Static_Data.CategoryMessages.categoryCreated;
                }
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Data.Static_Data.CategoryMessages.failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == user.CompanyId);
            var categoryList = _unitOfWork.Category.GetAll(u => u.CompanyId == company.Id);
            return Json(new { data = categoryList });
        }

        [HttpGet]
        [Route("GetCategoryData/{id}")]
        public IActionResult GetCategoryData(int id)
        {
            CommonResponse<Category> commonResponse = new CommonResponse<Category>();
            try
            {
                commonResponse.dataenum = _categoryService.GetById(id);
                commonResponse.status = Data.Static_Data.CategoryMessages.success_code;
            }
            catch(Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Data.Static_Data.CategoryMessages.failure_code;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("DeleteCategory/{id}")]
        public IActionResult DeleteCategory(int id)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _categoryService.DeleteCategory(id).Result;
                if (commonResponse.status == 1)
                {
                    // Delete
                    commonResponse.message = Data.Static_Data.CategoryMessages.categoryDeleted;
                }
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Data.Static_Data.CategoryMessages.failure_code;
            }
            return Ok(commonResponse);
        }
    }
}