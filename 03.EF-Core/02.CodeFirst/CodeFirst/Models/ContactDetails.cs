namespace CodeFirst.Models
{
    public class ContactDetails
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public ContactLocation Location { get; set; }

        // Navigation property for 1-to-1 relation, note that for 1-to-1 relation EF Core requires
        // definition of the both properties: navigation and inverse. Both of this properties
        // must declare single object not collection. This will be dependet entity, so the table
        // must contain foreign key column.
        // NOTE: The 1-to-1 relation applies the unique index on FK column with (1-to-0|1) or
        //       without (1-to-1) filtered out NULL values
        public long? SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
