using System.Linq;
using Xunit;
using Moq;
using SportsStore.Controllers;
using SportsStore.Models;
using SportsStore.Models.ViewModels;

namespace SportsStore.Tests
{
    public class ProductControllerTests
    {
        [Fact]
        public void CanPaginate()
        {
            Product[] expectedProducts = {
                new Product {Id = 4, Name = "P4"},
                new Product {Id = 5, Name = "P5"}
            };

            var pageSize = 3;
            var expectedPage = 2;
            var expectedPagesCount = 2;
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {Id = 1, Name = "P1"},
                new Product {Id = 2, Name = "P2"},
                new Product {Id = 3, Name = "P3"},
                expectedProducts[0],
                expectedProducts[1]
            }).AsQueryable<Product>());

            var productController = new ProductController(mock.Object)
            {
                PageSize = pageSize
            };

            var result = productController.List(null, expectedPage).ViewData.Model as ProductListViewModel;

            Assert.NotNull(result);

            var actualProducts = result.Products.ToArray();
            var actualPagingInfo = result.PagingInfo;

            Assert.Equal(expectedProducts, actualProducts);
            Assert.Equal(expectedPage, actualPagingInfo.CurrentPage);
            Assert.Equal(pageSize, actualPagingInfo.ItemsPerPage);
            Assert.Equal(mock.Object.Products.Count(), actualPagingInfo.TotalItems);
            Assert.Equal(expectedPagesCount, actualPagingInfo.TotalPages);
        }

        [Fact]
        public void CanFilterByCategory()
        {
            var targetCategory = "C2";
            var targetPage = 1;
            Product[] expectedProducts = {
                new Product {Id = 4, Category = targetCategory},
                new Product {Id = 5, Category = targetCategory}
            };

            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {Id = 1, Name = "P1", Category = "C1"},
                expectedProducts[0],
                new Product {Id = 3, Name = "P3", Category = "C1"},
                expectedProducts[1],
                new Product {Id = 5, Name = "P5", Category = "C1"}
            }).AsQueryable<Product>());

            var productController = new ProductController(mock.Object);
            var result = productController.List(targetCategory, targetPage).ViewData.Model as ProductListViewModel;

            Assert.NotNull(result);

            var actualProducts = result.Products.ToArray();
            var actualPagingInfo = result.PagingInfo;

            Assert.Equal(expectedProducts, actualProducts);
            Assert.Equal(expectedProducts.Count(), actualPagingInfo.TotalItems);
        }
    }
}
