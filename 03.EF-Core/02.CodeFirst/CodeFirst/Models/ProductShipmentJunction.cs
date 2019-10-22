namespace CodeFirst.Models
{
    // Many-to-many relation is created through junction class which have two 1-to-many relation
    public class ProductShipmentJunction
    {
        public long Id { get; set; }

        public long ProductId { get; set; }
        public Product Product { get; set; }

        public long ShipmentId { get; set; }
        public Shipment Shipment { get; set; }
    }
}
