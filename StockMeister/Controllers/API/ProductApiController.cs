using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMeister.Data.Repository.IRepository;
using StockMeister.Data.Services;
using StockMeister.Models;
using StockMeister.Models.ViewModels;
using System.Security.Claims;

namespace StockMeister.Controllers.API
{
    [Route("Controllers/api/Product")]
    [ApiController]
    public class ProductApiController : Controller
    {
        private readonly IProductService _productService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly string loginUserId;
        private readonly string role;
        public ProductApiController(IProductService productService, IHttpContextAccessor httpContextAccessor, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
        {
            _productService = productService;
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            loginUserId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            role = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Role).Value;
        }

        [HttpPost]
        [Route("SaveProductData")]
        public IActionResult SaveProductData(ProductVM data)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _productService.AddUpdate(data).Result;
                if (commonResponse.status == 1)
                {
                    // Update
                    commonResponse.message = Data.Static_Data.ProductMessages.productUpdated;
                }
                if (commonResponse.status == 2)
                {
                    // Create
                    commonResponse.message = Data.Static_Data.ProductMessages.productCreated;
                }
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Data.Static_Data.ProductMessages.failure_code;
            }
            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == user.CompanyId);
            var productList = _unitOfWork.Product.GetAll(u => u.CompanyId == company.Id, "Category");
            return Json(new { data = productList });
        }

        [HttpGet]
        [Route("GetProductData/{id}")]
        public  IActionResult GetProductData(int id)
        {
            CommonResponse<Product> commonResponse = new CommonResponse<Product>();
            try
            {
                commonResponse.dataenum = _productService.GetById(id);
                commonResponse.status = Data.Static_Data.ProductMessages.success_code;
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Data.Static_Data.ProductMessages.failure_code;
            }

            return Ok(commonResponse);
        }

        [HttpGet]
        [Route("DeleteProduct/{id}")]
        public IActionResult DeleteProduct(int id)
        {
            CommonResponse<int> commonResponse = new CommonResponse<int>();
            try
            {
                commonResponse.status = _productService.DeleteProduct(id).Result;
                if(commonResponse.status == 1)
                {
                    // Delete
                    commonResponse.message = Data.Static_Data.ProductMessages.productDeleted;
                }
            }
            catch (Exception e)
            {
                commonResponse.message = e.Message;
                commonResponse.status = Data.Static_Data.ProductMessages.failure_code;
            }
            return Ok(commonResponse);
        }
    }
}