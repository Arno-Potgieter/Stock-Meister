using Microsoft.AspNetCore.Mvc;
using StockMeister.Data.Services;
using StockMeister.Models;
using StockMeister.Models.ViewModels;
using System.Security.Claims;

namespace StockMeister.Controllers.API
{
    [Route("api/Category")]
    [ApiController]
    public class CategoryApiController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string loginUserId;
        private readonly string role;
        public CategoryApiController(ICategoryService categoryService, IHttpContextAccessor httpContextAccessor)
        {
            _categoryService = categoryService;
            _httpContextAccessor = httpContextAccessor;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
        }

        [HttpPost]
        [Route("SaveCategoryData")]
        public IActionResult SaveCategoryData(Category data)
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
    }
}
