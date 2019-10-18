using System.Collections.Generic;

namespace CodeFirst.Models
{
    public interface ICustomerRepository
    {
        IEnumerable<Customer> GetAllCustomers();
    }

    public class EfCustomerRepository : ICustomerRepository
    {
        private EfCustomerContext _dbContext;

        public EfCustomerRepository(EfCustomerContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _dbContext.Customers;
        }
    }
}
