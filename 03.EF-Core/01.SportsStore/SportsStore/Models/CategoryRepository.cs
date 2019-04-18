using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SportsStore.Models.Pages;

namespace SportsStore.Models
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> AllCategories { get; }
        PagedList<Category> GetCategories(QueryOptions options);
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }

    public class DbCategoryRepository : ICategoryRepository
    {
        private readonly AppDbContext _dbContext;

        public DbCategoryRepository(AppDbContext dbContext) => _dbContext = dbContext;

        public IEnumerable<Category> AllCategories => _dbContext.Categories;

        public PagedList<Category> GetCategories(QueryOptions options)
        {
            return new PagedList<Category>(_dbContext.Categories, options);
        }

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
