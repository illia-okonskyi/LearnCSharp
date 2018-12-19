using System;
using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class CartLine
    {
        public int Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

    public class Cart
    {
        private List<CartLine> _lines = new List<CartLine>();
        public virtual void AddItem(Product product, int quantity)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            if (quantity <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(quantity), "Must be greater than 0");
            }

            var line = _lines.Where(p => p.Product.Id == product.Id).FirstOrDefault();
            if (line != null)
            {
                line.Quantity += quantity;
                return;
            }

            _lines.Add(new CartLine
            {
                Product = product,
                Quantity = quantity
            });

        }

        public virtual void RemoveLine(Product product)
        {
            if (product == null)
            {
                throw new ArgumentNullException(nameof(product));
            }

            _lines.RemoveAll(l => l.Product.Id == product.Id);
        }

        public virtual decimal CalcTotalPrice() => _lines.Sum(l => l.Product.Price * l.Quantity);
        public virtual void Clear() => _lines.Clear();
        public virtual IEnumerable<CartLine> Lines => _lines;
    }
}
