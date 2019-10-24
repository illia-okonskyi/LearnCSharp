using System.Collections.Generic;

namespace ModelFirst.Models.Manual
{
    // Fittings table
    public class ShoeWidth
    {
        // Id column
        public long UniqueIdent { get; set; }

        // Name column
        public string WidthName { get; set; }

        // Inverse property
        public IEnumerable<Shoe> Products { get; set; }
    }
}
