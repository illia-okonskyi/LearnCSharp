namespace CodeFirst.Models
{
    public enum Colors
    {
        Red, Green, Blue
    }

    public class Product
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public Colors Color { get; set; }
        public bool InStock { get; set; }

        // Foreign key property, Product -> Supplier. Reflects real column in the table.
        // Default convention EF Core relies is to make the name of the FK property like
        // <NavigationPropertyName>Id. The type of the FK property tells EF Core whether the
        // relationship is optional or required. Nullable type of the FK property reflects nullable
        // column in the table.
        public long SupplierId { get; set; }
        // Navigation property, allows navigation from one object to another: Product -> Supplier
        public Supplier Supplier { get; set; }
    }
}
