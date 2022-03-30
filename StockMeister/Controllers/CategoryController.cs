using Microsoft.AspNetCore.Mvc;
using StockMeister.Data.Repository.IRepository;

namespace StockMeister.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult GetCreate()
        {
            return View();
        }
    }
}
