using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using SportsStore.Models;
using SportsStore.Controllers;

namespace SportsStore.Tests
{
    public class AdminControllerTests
    {
        [Fact]
        public void Index_ExactProductList()
        {
            Product [] expectedProducts = {
                new Product {Id = 1},
                new Product {Id = 2},
                new Product {Id = 3}
            };

            var mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns(expectedProducts.AsQueryable<Product>());
            var adminController = new AdminController(mock.Object);

            var actualProducts = GetViewModel<IEnumerable<Product>>(adminController.Index());
            Assert.Equal(expectedProducts, actualProducts);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
