using NepaliBazar.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NepaliBazar.DataAccess.Repository.IRepository
{
    public interface IShoppingCartRepository : IRepository<ShoppingCart>
    {
        int incrementCount(ShoppingCart shoppingCart, int count);
        int decrementCount(ShoppingCart shoppingCart, int count);

    }
}
