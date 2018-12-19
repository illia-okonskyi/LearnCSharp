using System;
using System.Linq;
using Xunit;
using SportsStore.Models;

namespace SportsStore.Tests
{
    public class CartTests
    {
        [Fact]
        public void AddItem_ArgumentCheck()
        {
            var p = new Product { Id = 1 };
            var cart = new Cart();

            Assert.Throws<ArgumentNullException>(() => cart.AddItem(null, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => cart.AddItem(p, 0));
            Assert.Throws<ArgumentOutOfRangeException>(() => cart.AddItem(p, -1));
        }

        [Fact]
        public void AddItem_NewLines()
        {
            Product[] expectedProdcuts = { new Product { Id = 1 }, new Product { Id = 2 } };

            var cart = new Cart();

            for (int i = 0; i < expectedProdcuts.Length; ++i)
            {
                cart.AddItem(expectedProdcuts[i], 1);
            }

            var lines = cart.Lines;
            Assert.Equal(expectedProdcuts.Count(), lines.Count());
            for (int i = 0; i < expectedProdcuts.Length; ++i)
            {
                Assert.Equal(expectedProdcuts[i], lines.ElementAt(i).Product);
            }
        }

        [Fact]
        public void AddItem_ExisitingLine()
        {
            var p = new Product { Id = 1 };
            var initialQuatity = 2;
            var addQuantity = 3;
            var expectedQuatity = 5;


            var cart = new Cart();
            cart.AddItem(p, initialQuatity);
            cart.AddItem(p, addQuantity);

            var lines = cart.Lines;

            Assert.Single(lines);
            Assert.Equal(expectedQuatity, lines.ElementAt(0).Quantity);
        }

        [Fact]
        public void RemoveLine_ArgumentCheck()
        {
            var cart = new Cart();

            Assert.Throws<ArgumentNullException>(() => cart.RemoveLine(null));
        }

        [Fact]
        public void RemoveLine()
        {
            var p1 = new Product { Id = 1 };
            var p2 = new Product { Id = 2 };

            var cart = new Cart();
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);

            var lines = cart.Lines;
            Assert.Equal(2, lines.Count());
            cart.RemoveLine(p2);
            Assert.Single(lines);
            Assert.Equal(p1, lines.ElementAt(0).Product);
        }

        [Fact]
        public void CalcTotalPrice()
        {
            var p1 = new Product { Id = 1, Price = 100 };
            var p2 = new Product { Id = 2, Price = 200 };
            var p1Quatity = 1;
            var p2Quatity = 3;
            var expectedTotalPrice = p1.Price * p1Quatity + p2.Price * p2Quatity;

            var cart = new Cart();
            Assert.Equal(0, cart.CalcTotalPrice());
            cart.AddItem(p1, p1Quatity);
            cart.AddItem(p2, p2Quatity);
            Assert.Equal(expectedTotalPrice, cart.CalcTotalPrice());
        }

        [Fact]
        public void Clear()
        {
            var p1 = new Product { Id = 1, Price = 100 };
            var p2 = new Product { Id = 2, Price = 200 };

            var cart = new Cart();
            Assert.Empty(cart.Lines);
            cart.AddItem(p1, 1);
            cart.AddItem(p2, 1);

            Assert.NotEmpty(cart.Lines);
            cart.Clear();
            Assert.Empty(cart.Lines);
        }
    }
}
