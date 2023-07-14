using Mishi.DataAccess.Data;
using Mishi.DataAccess.Repository.IRepository;
using Mishi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Mishi.DataAccess.Repository
{
    public class OrderDetailsRepository : Repository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly ApplicationDbContext _db;
        public OrderDetailsRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }
   
        public void Update(OrderDetails orderDetails)
        {
            _db.OrderDetails.Update(orderDetails);

        }
    }
}
