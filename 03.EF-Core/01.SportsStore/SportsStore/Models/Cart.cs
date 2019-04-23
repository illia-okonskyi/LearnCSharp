using System.Collections.Generic;
using System.Linq;

namespace SportsStore.Models
{
    public class Cart
    {
        private readonly List<OrderLine> _selections = new List<OrderLine>();

        public IEnumerable<OrderLine> Selections { get => _selections; }

        public Cart AddItem(Product product, int quantity)
        {
            var line = _selections.Where(l => l.ProductId == product.Id).FirstOrDefault();
            if (line != null)
            {
                line.Quantity += quantity;
                return this;
            }

            _selections.Add(new OrderLine {
                ProductId = product.Id,
                Product = product,
                Quantity = quantity
            });
            return this;
        }

        public Cart RemoveItem(long productId)
        {
            _selections.RemoveAll(l => l.ProductId == productId);
            return this;
        }

        public void Clear() => _selections.Clear();
    }
}
