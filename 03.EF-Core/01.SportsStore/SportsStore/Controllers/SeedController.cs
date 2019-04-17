using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SportsStore.Models;

namespace SportsStore.Controllers
{
    public class SeedController : Controller
    {
        private AppDbContext _dbContext;
        public SeedController(AppDbContext dbContext) => _dbContext = dbContext;
        public IActionResult Index()
        {
            ViewBag.Count = _dbContext.Products.Count();
            return View(_dbContext.Products.Include(p => p.Category).OrderBy(p => p.Id).Take(20));
        }

        [HttpPost]
        public IActionResult CreateSeedData(int count)
        {
            ClearData();
            if (count > 0)
            {
                _dbContext.Database.SetCommandTimeout(System.TimeSpan.FromMinutes(10));
                _dbContext.Database.ExecuteSqlCommand("DROP PROCEDURE IF EXISTS CreateSeedData");
                _dbContext.Database.ExecuteSqlCommand($@"
                    CREATE PROCEDURE CreateSeedData
                    @RowCount decimal AS
                    BEGIN
                        SET NOCOUNT ON
                        DECLARE @i INT = 1;
                        DECLARE @catId BIGINT;
                        DECLARE @CatCount INT = @RowCount / 10;
                        DECLARE @pprice DECIMAL(5,2);
                        DECLARE @rprice DECIMAL(5,2);
                        BEGIN TRANSACTION
                            WHILE @i <= @CatCount
                            BEGIN
                            INSERT INTO Categories (Name, Description)
                            VALUES (CONCAT('Category-', @i), 'Test Data Category');
                            SET @catId = SCOPE_IDENTITY();
                            DECLARE @j INT = 1;
                            WHILE @j <= 10
                            BEGIN
                                SET @pprice = RAND()*(500-5+1);
                                SET @rprice = (RAND() * @pprice) + @pprice;
                                INSERT INTO Products (Name, CategoryId, PurchasePrice, RetailPrice)
                                VALUES (CONCAT('Product', @i, '-', @j), @catId, @pprice, @rprice)
                                SET @j = @j + 1
                            END
                            SET @i = @i + 1
                        END
                        COMMIT
                    END");
                _dbContext.Database.BeginTransaction();
                _dbContext.Database.ExecuteSqlCommand($"EXEC CreateSeedData @RowCount = {count}");
                _dbContext.Database.CommitTransaction();
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public IActionResult ClearData()
        {
            _dbContext.Database.SetCommandTimeout(System.TimeSpan.FromMinutes(10));
            _dbContext.Database.BeginTransaction();
            _dbContext.Database.ExecuteSqlCommand("DELETE FROM Orders");
            _dbContext.Database.ExecuteSqlCommand("DELETE FROM Categories");
            _dbContext.Database.CommitTransaction();
            return RedirectToAction(nameof(Index));
        }
    }
}
