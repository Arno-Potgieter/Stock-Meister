using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using StockMeister.Data.Repository.IRepository;
using StockMeister.Data.Services;
using StockMeister.Models;
using StockMeister.Models.ViewModels;
using System.Security.Claims;

namespace StockMeister.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public ProductController(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var company = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == user.CompanyId);

            IEnumerable<SelectListItem> CategoryList = _unitOfWork.Category.GetAll(u => u.CompanyId == company.Id).Select(
                u => new SelectListItem
                {
                    Text = u.CategoryName,
                    Value = u.Id.ToString(),
                }
            );
            ViewBag.CategoryList = CategoryList;
            ViewBag.companyName = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == user.CompanyId);
            return View();
        }
    }
}
