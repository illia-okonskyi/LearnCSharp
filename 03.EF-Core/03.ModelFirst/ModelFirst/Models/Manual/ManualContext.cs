using Microsoft.EntityFrameworkCore;

namespace ModelFirst.Models.Manual
{
    // NOTE: The Attributes for Overriding the Basic Data Model Conventions. Are applied directly
    //       to the entities. See Style.cs file
    //       - Table - This attribute specifies the database table and overrides the name of the
    //                 property in the context class.
    //       - Column - This attribute specifies the column that provides values for the property it
    //                  is applied to.
    //       - Key - This attribute is used to identify the property that will be assigned the
    //               primary key value.
    //       - ForeignKey - This attribute is used to identify the foreign key property for a
    //                      navigation property.
    //       - InverseProperty - This attribute is used to specify the name of the property at the
    //                           end of the relationship.

    public class ManualContext : DbContext
    {
        public ManualContext(DbContextOptions<ManualContext> options)
            : base(options) { }

        public DbSet<Shoe> Shoes { get; set; }
        public DbSet<Style> ShoeStyles { get; set; }
    }
}
