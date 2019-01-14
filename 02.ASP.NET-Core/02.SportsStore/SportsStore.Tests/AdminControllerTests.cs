using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
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

        [Fact]
        public void Edit_ExactProduct()
        {
            var expectedProduct1 = new Product { Id = 1 };
            var expectedProduct2 = new Product { Id = 2 };
            var expectedProduct3 = new Product { Id = 3 };
            Product[] expectedProducts = { expectedProduct1, expectedProduct2, expectedProduct3 };

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(m => m.Products).Returns(expectedProducts.AsQueryable<Product>());
            var adminController = new AdminController(productRepositoryMock.Object);

            var actualProduct1 = GetViewModel<Product>(adminController.Edit(1));
            var actualProduct2 = GetViewModel<Product>(adminController.Edit(2));
            var actualProduct3 = GetViewModel<Product>(adminController.Edit(3));

            Assert.Equal(expectedProduct1, actualProduct1);
            Assert.Equal(expectedProduct2, actualProduct2);
            Assert.Equal(expectedProduct3, actualProduct3);
        }

        [Fact]
        public void Edit_NonExistingProduct()
        {
            Product[] existingProducts = { new Product { Id = 1 } };
            int nonExistingId = 2;

            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(m => m.Products).Returns(existingProducts.AsQueryable<Product>());
            var adminController = new AdminController(productRepositoryMock.Object);

            var nonExistingProduct = GetViewModel<Product>(adminController.Edit(nonExistingId));

            Assert.Null(nonExistingProduct);
        }

        [Fact]
        public void Edit_Post_ValidProduct()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var tempDataMock = new Mock<ITempDataDictionary>();
            var adminController = new AdminController(productRepositoryMock.Object)
            {
                TempData = tempDataMock.Object
            };


            var product = new Product { Id = 1 };
            var result = adminController.Edit(product);

            productRepositoryMock.Verify(m => m.SaveProduct(product));
            Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", (result as RedirectToActionResult).ActionName);
        }

        [Fact]
        public void Edit_Post_InvalidProduct()
        {
            var productRepositoryMock = new Mock<IProductRepository>();
            var adminController = new AdminController(productRepositoryMock.Object);

            adminController.ModelState.AddModelError("error", "error");

            var product = new Product { Id = 1 };
            var result = adminController.Edit(product);

            productRepositoryMock.Verify(m => m.SaveProduct(It.IsAny<Product>()), Times.Never());
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Delete_ValidProductId()
        {
            int targetProductId = 2;
            Product[] products = { new Product { Id = 1 }, new Product { Id = targetProductId }};
            
            var productRepositoryMock = new Mock<IProductRepository>();
            productRepositoryMock.Setup(m => m.Products).Returns(products.AsQueryable<Product>());
            var adminController = new AdminController(productRepositoryMock.Object);

            adminController.Delete(targetProductId);

            productRepositoryMock.Verify(m => m.DeleteProduct(targetProductId), Times.Once);
        }

        private T GetViewModel<T>(IActionResult result) where T : class
        {
            return (result as ViewResult)?.ViewData.Model as T;
        }
    }
}
