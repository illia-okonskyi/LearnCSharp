using System.Collections.Generic;

namespace CodeFirst.Models
{
    public class Supplier
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public ContactDetails Contact { get; set; }

        // Inverse property is required to complete the 1-to-many relationship
        // For completing the 1-to-many relationship new migration is not required
        public IEnumerable<Product> Products { get; set; }
    }
}
