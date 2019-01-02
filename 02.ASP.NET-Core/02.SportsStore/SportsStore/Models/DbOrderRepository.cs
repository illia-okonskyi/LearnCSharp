using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace SportsStore.Models
{
    public class DbOrderRepository : IOrderRepository
    {
        private ApplicationDbContext _dbContext;

        public DbOrderRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Order> Orders
        {
            get
            {
                return _dbContext.Orders.Include(o => o.CartLines).ThenInclude(l => l.Product);
            }
        }

        public void SaveOrder(Order order)
        {
            _dbContext.AttachRange(order.CartLines.Select(l => l.Product));
            if (order.Id == 0)
            {
                _dbContext.Orders.Add(order);
            }
            _dbContext.SaveChanges();
        }
    }
}
