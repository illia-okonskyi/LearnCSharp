using System.Collections.Generic;

namespace CodeFirst.Models
{
    public interface IGenericRepository<T> where T : class
    {
        T Get(long id);
        IEnumerable<T> GetAll();
        void Create(T newDataObject);
        void Update(T changedDataObject);
        void Delete(long id);
    }

    public class EfGenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected EfDbContext _dbContext;

        public EfGenericRepository(EfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual T Get(long id)
        {
            return _dbContext.Set<T>().Find(id);
        }
        public virtual IEnumerable<T> GetAll()
        {
            return _dbContext.Set<T>();
        }

        public virtual void Create(T newDataObject)
        {
            _dbContext.Add<T>(newDataObject);
            _dbContext.SaveChanges();
        }
        public virtual void Delete(long id)
        {
            _dbContext.Remove<T>(Get(id));
            _dbContext.SaveChanges();
        }
        public virtual void Update(T changedDataObject)
        {
            _dbContext.Update<T>(changedDataObject);
            _dbContext.SaveChanges();
        }
    }
}
