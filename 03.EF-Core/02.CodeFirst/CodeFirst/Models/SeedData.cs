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
                    productContext.Products.AddRange(Products);
                }
                else if (context is EfCustomerContext customerContext && customerContext.Customers.Count() == 0)
                {
                    customerContext.Customers.AddRange(Customers);
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

        private static Product[] Products
        {
            get
            {
                var products = new Product[]
                {
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

                var hq = new ContactLocation
                {
                    LocationName = "Corporate HQ",
                    Address = "200 Acme Way"
                };
                var bob = new ContactDetails
                {
                    Name = "Bob Smith",
                    Phone = "555-107-1234",
                    Location = hq
                };
                var acme = new Supplier
                {
                    Name = "Acme Co",
                    City = "New York",
                    State = "NY",
                    Contact = bob
                };
                var s1 = new Supplier
                {
                    Name = "Surf Dudes",
                    City = "San Jose",
                    State = "CA"
                };
                var s2 = new Supplier
                {
                    Name = "Chess Kings",
                    City = "Seattle",
                    State = "WA"
                };

                foreach (var p in products)
                {
                    if (p == products[0])
                    {
                        p.Supplier = s1;
                    }
                    else if (p.Category == "Chess")
                    {
                        p.Supplier = s2;
                    }
                    else
                    {
                        p.Supplier = acme;
                    }
                }
                return products;
            }
        }

        private static Customer[] Customers
        {
            get
            {
                var customers = new Customer[]
                {
                    new Customer { Name = "Alice Smith", City = "New York", Country = "USA" },
                    new Customer { Name = "Bob Jones", City = "Paris", Country = "France" },
                    new Customer { Name = "Charlie Davies", City = "London", Country = "UK" }
                };
                return customers;
            }
        }
    }
}
