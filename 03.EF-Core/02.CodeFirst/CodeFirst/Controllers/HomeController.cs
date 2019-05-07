using Microsoft.AspNetCore.Mvc;
using CodeFirst.Models;

namespace CodeFirst.Controllers
{
    public class HomeController : Controller
    {
        private readonly EfDbContext _dbContext;

        public HomeController(EfDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View(_dbContext.Products);
        }
    }
}
