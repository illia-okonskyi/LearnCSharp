using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Moq;
using Xunit;
using SportsStore.Components;
using SportsStore.Models;

namespace SportsStore.Tests
{
    public class NavigationMenuViewComponentTests
    {
        [Fact]
        public void CanSelectCategories()
        {
            string[] expectedCategories = { "C1", "C2", "C3" };
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {Id = 1, Name = "P1", Category = expectedCategories[0]},
                new Product {Id = 2, Name = "P2", Category = expectedCategories[0]},
                new Product {Id = 3, Name = "P3", Category = expectedCategories[2]},
                new Product {Id = 4, Name = "P4", Category = expectedCategories[1]}
            }).AsQueryable<Product>());
            var navigationMenu = new NavigationMenuViewComponent(mock.Object);

            var model = (navigationMenu.Invoke() as ViewViewComponentResult).ViewData.Model;
            var actualCategories = (model as IEnumerable<string>).ToArray();

            Assert.Equal(expectedCategories, actualCategories);
        }

        [Fact]
        public void IndicatesSelectedCategory()
        {
            string[] categories = { "C1", "C2" };
            string expectedCategory = categories[1];
            Mock<IProductRepository> mock = new Mock<IProductRepository>();
            mock.Setup(m => m.Products).Returns((new Product[] {
                new Product {Id = 1, Name = "P1", Category = categories[0]},
                new Product {Id = 2, Name = "P2", Category = expectedCategory}
            }).AsQueryable<Product>());
            var navigationMenu = new NavigationMenuViewComponent(mock.Object)
            {
                ViewComponentContext = new ViewComponentContext
                {
                    ViewContext = { RouteData = new RouteData() }
                }
            };
            navigationMenu.RouteData.Values["category"] = expectedCategory;

            Assert.Null(navigationMenu.ViewBag.SelectedCategory);
            navigationMenu.Invoke();
            Assert.Equal(expectedCategory, navigationMenu.ViewBag.SelectedCategory);
        }
    }
}
