using System.Collections.Generic;

namespace CodeFirst.Models
{
    public class Supplier
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        // Inverse property for 1-to-1 relation, note that for 1-to-1 relation EF Core requires
        // definition of the both properties: navigation and inverse. Both of this properties
        // must declare single object not collection. This will be principal entity, so the table
        // must not contain foreign key column.
        public ContactDetails Contact { get; set; }

        // Inverse property is required to complete the 1-to-many relationship
        // For completing the 1-to-many relationship new migration is not required
        public IEnumerable<Product> Products { get; set; }
    }
}
