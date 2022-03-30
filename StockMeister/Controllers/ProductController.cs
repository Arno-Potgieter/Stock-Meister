using Microsoft.AspNetCore.Mvc;

namespace StockMeister.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
