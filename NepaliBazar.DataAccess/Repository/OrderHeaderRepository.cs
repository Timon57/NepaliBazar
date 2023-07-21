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
    public class OrderHeaderRepository : Repository<OrderHeader>, IOrderHeaderRepository
    {
        private AppDbContext _db;
        public OrderHeaderRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }
      

        public void Update(OrderHeader obj)
        {
            _db.OrderHeaders.Update(obj);
        }

        public void UpdateStatus(int id, string orderStatus, string? paymentStatus = null)
        {
            var OrderFormDb = _db.OrderHeaders.FirstOrDefault(x => x.Id == id);
            if(OrderFormDb != null)
            {
                OrderFormDb.OrderStatus = orderStatus;
                if (paymentStatus != null)
                {
                    OrderFormDb.PaymentStatus = paymentStatus;
                }
            }
            
        }
    }
}
