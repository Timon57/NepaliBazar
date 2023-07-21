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
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        private AppDbContext _db;
        public ShoppingCartRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public int decrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count -= count;
            return shoppingCart.Count;
        }

        public int incrementCount(ShoppingCart shoppingCart, int count)
        {
            shoppingCart.Count += count;
            return shoppingCart.Count;
        }
    }
}
