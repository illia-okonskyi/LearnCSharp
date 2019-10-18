using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CodeFirst.Models
{
    public static class SeedData
    {
        public static void Seed(DbContext context)
        {
            if (context.Database.GetPendingMigrations().Count() == 0)
            {
                if (context is EfDbContext productContext && productContext.Products.Count() == 0)
                {
                    foreach (var p in _products)
                    {
                        p.Id = 0;
                    }
                    productContext.Products.AddRange(_products);
                }
                else if (context is EfCustomerContext customerContext && customerContext.Customers.Count() == 0)
                {
                    foreach (var c in _customers)
                    {
                        c.Id = 0;
                    }
                    customerContext.Customers.AddRange(_customers);
                }
                context.SaveChanges();
            }
        }

        public static void ClearData(DbContext context)
        {
            if (context is EfDbContext productContext && productContext.Products.Count() > 0)
            {
                productContext.Products.RemoveRange(productContext.Products);
            }
            else if (context is EfCustomerContext customerContext && customerContext.Customers.Count() > 0)
            {
                customerContext.Customers.RemoveRange(customerContext.Customers);
            }
            context.SaveChanges();
        }

        private static readonly Product[] _products = {
            new Product { Name = "Kayak", Category = "Watersports", Price = 275, Color = Colors.Green, InStock = true },
            new Product { Name = "Lifejacket", Category = "Watersports", Price = 48.95m, Color = Colors.Red, InStock = true },
            new Product { Name = "Soccer Ball", Category = "Soccer", Price = 19.50m, Color = Colors.Blue, InStock = true },
            new Product { Name = "Corner Flags", Category = "Soccer", Price = 34.95m, Color = Colors.Green, InStock = true },
            new Product { Name = "Stadium", Category = "Soccer", Price = 79500, Color = Colors.Red, InStock = true },
            new Product { Name = "Thinking Cap", Category = "Chess", Price = 16, Color = Colors.Blue, InStock = true },
            new Product { Name = "Unsteady Chair", Category = "Chess", Price = 29.95m, Color = Colors.Green, InStock = true },
            new Product { Name = "Human Chess Board", Category = "Chess", Price = 75, Color = Colors.Red, InStock = true },
            new Product { Name = "Bling-Bling King", Category = "Chess", Price = 1200, Color = Colors.Blue, InStock = true }
        };

        private static readonly Customer[] _customers = {
            new Customer { Name = "Alice Smith", City = "New York", Country = "USA" },
            new Customer { Name = "Bob Jones", City = "Paris", Country = "France" },
            new Customer { Name = "Charlie Davies", City = "London", Country = "UK" }
        };
    }
}
