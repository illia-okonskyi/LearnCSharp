using System.Collections.Generic;

namespace CodeFirst.Models
{
    public interface ISupplierRepository
    {
        Supplier Get(long id);
        IEnumerable<Supplier> GetAll();
        void Create(Supplier newDataObject);
        void Update(Supplier changedDataObject);
        void Delete(long id);
    }

    public class EfSupplierRepository : ISupplierRepository
    {
        private EfDbContext _dbContext;

        public EfSupplierRepository(EfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Supplier Get(long id)
        {
            return _dbContext.Suppliers.Find(id);
        }

        public IEnumerable<Supplier> GetAll()
        {
            return _dbContext.Suppliers;
        }

        public void Create(Supplier newDataObject)
        {
            _dbContext.Add(newDataObject);
            _dbContext.SaveChanges();
        }

        public void Update(Supplier changedDataObject)
        {
            _dbContext.Update(changedDataObject);
            _dbContext.SaveChanges();
        }

        public void Delete(long id)
        {
            _dbContext.Remove(Get(id));
            _dbContext.SaveChanges();
        }
    }
}
