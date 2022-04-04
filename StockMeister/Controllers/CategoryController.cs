using Microsoft.AspNetCore.Mvc;
using StockMeister.Data.Repository.IRepository;
using StockMeister.Models;

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
            IEnumerable<Category> categoryList = _unitOfWork.Category.GetAll();
            return View(categoryList);
        }
    }
}
