using BookIt.DataAccess.Data;
using BookIt.DataAccess.Repository.IRepository;
using BookIt.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookIt.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private ApplicationDbContext _db;
        public ProductRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Product obj)
        {
            var objFromDb =_db.Products.FirstOrDefault(x => x.Id == obj.Id);
            if (objFromDb != null) 
            { 
                objFromDb.Title = obj.Title;
                objFromDb.BookNumber = obj.BookNumber;
                objFromDb.Price = obj.Price;
                objFromDb.Price50 = obj.Price50;
                objFromDb.Price100 = obj.Price100;
                objFromDb.ListPrice = obj.ListPrice;
                objFromDb.Author = obj.Author;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.ImageUrl = obj.ImageUrl;
                
            }
        }
    }
}
