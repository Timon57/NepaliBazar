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
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;
        public UnitOfWork(AppDbContext db) 
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Product  = new ProductRepository(_db);
            ShoppingCart = new ShoppingCartRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
        }
        public ICategoryRepository Category { get; private set; }
        public IProductRepository Product { get; private set; }

        public IShoppingCartRepository ShoppingCart { get; private set; }

        public IOrderDetailRepository OrderDetail { get; private set; }
        
        public IOrderHeaderRepository OrderHeader { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
