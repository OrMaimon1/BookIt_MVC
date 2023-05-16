using BookItWeb.Data;
using BookItWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookItWeb.Controllers
{
    public class CategoryController : Controller
    { 
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db) {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create() { 
        
            return View();  
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            if (obj.DisplayOrder.Equals(0))
            {
                ModelState.Remove("DisplayOrder");
                ModelState.AddModelError("DisplayOrder", "please enter a number");

            }

            return View();
        }

    }
}
