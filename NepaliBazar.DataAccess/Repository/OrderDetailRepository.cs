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
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private AppDbContext _db;
        public OrderDetailRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
      

        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
