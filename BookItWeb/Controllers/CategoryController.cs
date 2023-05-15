using Microsoft.AspNetCore.Mvc;

namespace BookItWeb.Controllers
{
    public class CategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
