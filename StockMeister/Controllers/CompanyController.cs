using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMeister.Data.Repository.IRepository;
using StockMeister.Models;
using StockMeister.Models.ViewModels;
using System.Security.Claims;

namespace StockMeister.Controllers
{
    [Authorize]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<ApplicationUser> _userManager;
        public CompanyController(IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, UserManager<ApplicationUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
        public async Task<IActionResult> Index(Company obj)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            if (user.CompanyId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                obj = _unitOfWork.Company.GetFirstOrDefault(u => u.Id == user.CompanyId);
                return View(obj);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Company obj)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            _unitOfWork.Company.Add(obj);
            _unitOfWork.Save();
            user.CompanyId = obj.Id;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
               return RedirectToAction("Index", "Company");
            }
            return View(obj);
        }
    }
}
