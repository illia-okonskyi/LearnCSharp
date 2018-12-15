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
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[]
            {
                new Product {Id = 1, Name = "P1"},
                new Product {Id = 2, Name = "P2"},
                new Product {Id = 3, Name = "P3"},
                new Product {Id = 4, Name = "P4"},
                new Product {Id = 5, Name = "P5"}
            }).AsQueryable<Product>());

            var productController = new ProductController(mock.Object)
            {
                PageSize = 3
            };

            var result = productController.List(2).ViewData.Model as ProductListViewModel;

            Assert.NotNull(result);

            var products = result.Products.ToArray();
            var pagingInfo = result.PagingInfo;

            Assert.Equal(2, products.Length);
            Assert.Equal(4, products[0].Id);
            Assert.Equal(5, products[1].Id);
            Assert.Equal(2, pagingInfo.CurrentPage);
            Assert.Equal(3, pagingInfo.ItemsPerPage);
            Assert.Equal(5, pagingInfo.TotalItems);
            Assert.Equal(2, pagingInfo.TotalPages);
        }
    }
}
