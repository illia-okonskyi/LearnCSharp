using System.Collections.Generic;

namespace CodeFirst.Models
{
    public class Shipment
    {
        public long Id { get; set; }

        public string ShipperName { get; set; }
        public string StartCity { get; set; }
        public string EndCity { get; set; }

        // Inverse property for many-to-many relation
        public IEnumerable<ProductShipmentJunction> ProductShipments { get; set; }
    }
}
