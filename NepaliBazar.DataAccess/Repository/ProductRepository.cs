using NepaliBazar.DataAccess.Data;
using NepaliBazar.DataAccess.Repository.IRepository;
using NepaliBazar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepaliBazar.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly AppDbContext _db;
        public ProductRepository(AppDbContext db) :base(db)
        {
            _db = db;
        }
        public void Update(Product obj)
        {
            Console.WriteLine(obj.Id);
            Console.WriteLine(obj.Name);
            var objFromDb = _db.Products.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Name = obj.Name;
                objFromDb.Description = obj.Description;
                objFromDb.Price = obj.Price;
                objFromDb.Weight = obj.Weight;
                objFromDb.ImageUrl = obj.ImageUrl;
                objFromDb.DiscountedPrice = obj.DiscountedPrice;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Rating = obj.Rating;
                objFromDb.Brand = obj.Brand;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }

        }
    }
}
