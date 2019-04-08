using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SportsStore.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> Categories { get; }
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }

    public class DbCategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public DbCategoryRepository(AppDbContext dbContext) => _dbContext = dbContext;

        public IEnumerable<Category> Categories => _dbContext.Categories;

        public void AddCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            _dbContext.SaveChanges();
        }

        public void UpdateCategory(Category category)
        {
            _dbContext.Categories.Update(category);
            _dbContext.SaveChanges();
        }

        public void DeleteCategory(Category category)
        {
            _dbContext.Categories.Remove(category);
            _dbContext.SaveChanges();
        }
    }
}
