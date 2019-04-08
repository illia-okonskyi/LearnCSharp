using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SportsStore.Models
{
    public interface IOrdersRepository
    {
        IEnumerable<Order> Orders { get; }
        Order GetOrder(long key);
        void AddOrder(Order order);
        void UpdateOrder(Order order);
        void DeleteOrder(Order order);
    }

    public class DbOrdersRepository : IOrdersRepository
    {
        private readonly AppDbContext _dbContext;
        public DbOrdersRepository(AppDbContext dbContext) => _dbContext = dbContext;

        public IEnumerable<Order> Orders =>
            _dbContext.Orders.Include(o => o.Lines).ThenInclude(l => l.Product);

        public Order GetOrder(long id) =>
            _dbContext.Orders.Include(o => o.Lines).First(o => o.Id == id);

        public void AddOrder(Order order)
        {
            _dbContext.Orders.Add(order);
            _dbContext.SaveChanges();
        }

        public void UpdateOrder(Order order)
        {
            _dbContext.Orders.Update(order);
            _dbContext.SaveChanges();
        }

        public void DeleteOrder(Order order)
        {
            _dbContext.Orders.Remove(order);
            _dbContext.SaveChanges();
        }
    }
}
