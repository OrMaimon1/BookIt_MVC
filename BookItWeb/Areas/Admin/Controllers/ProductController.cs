using BookIt.DataAccess.Repository.IRepository;
using BookIt.Models;
using BookIt.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.IO;

namespace BookItWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _UnitOfWork;
        private readonly IWebHostEnvironment _WebHostEnvironment;

        public ProductController(IUnitOfWork unitOfWork,IWebHostEnvironment webHostEnvironment)
        {
            _UnitOfWork = unitOfWork;
            _WebHostEnvironment = webHostEnvironment; 
        }

        public IActionResult Index()
        {
            List<Product> objProductList = _UnitOfWork.Product.GetAll().ToList();
            
            return View(objProductList);
        }

        public IActionResult Upsert(int? id) 
        {
            ProductVM productVM = new()
            {
                CategoryList= _UnitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                }),
                Product = new Product()
            };
            if(id == null ||id == 0) 
            {
                return View(productVM);
            }
            else //update
            {
                productVM.Product = _UnitOfWork.Product.Get(u=> u.Id == id);
                return View(productVM);
            }
        }

        [HttpPost]
        public IActionResult Upsert(ProductVM obj, IFormFile? file) {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _WebHostEnvironment.WebRootPath;
                if(file != null) 
                { 
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, @"images\product");

                    using(var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    obj.Product.ImageUrl= @"\images\product"+fileName;
                    
                }
                _UnitOfWork.Product.Add(obj.Product);
                _UnitOfWork.Save();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else 
            {
                obj.CategoryList = _UnitOfWork.Category.GetAll().Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
                return View(obj);

            }
        }

        public IActionResult Delete(int? id) 
        {
            if(id == null || id ==0) 
            { 
                return NotFound();
            }
            Product? productFromDb = _UnitOfWork.Product.Get(u => u.Id ==id);
            if (productFromDb == null)
            {
                return NotFound();
            }
            return View(productFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePost(int? id) 
        {
            Product? obj = _UnitOfWork.Product.Get(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _UnitOfWork.Product.Remove(obj);
            _UnitOfWork.Save();
            TempData["success"] = "Product deleted successfully";
            return RedirectToAction("Index");
        }

    }
}
