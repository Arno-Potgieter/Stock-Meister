using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMeister.Data.Repository.IRepository;
using StockMeister.Models;

namespace StockMeister.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;

        public CategoryController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(_httpContextAccessor.HttpContext.User);
            if (user.CompanyId == null)
            {
                return RedirectToAction("Create", "Company");
            }
            ViewBag.companyName = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == user.CompanyId);
            return View();
        }
    }
}
