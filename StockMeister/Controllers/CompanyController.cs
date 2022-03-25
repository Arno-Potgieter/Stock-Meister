using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using StockMeister.Data.Repository.IRepository;
using StockMeister.Models;
using System.Security.Claims;

namespace StockMeister.Controllers
{
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {                      
            return View();
        }
    }
}
