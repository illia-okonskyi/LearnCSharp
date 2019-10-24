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
        public DbSet<ShoeWidth> ShoeWidths { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // FluentAPI alternative for [Table] attribute
            modelBuilder.Entity<ShoeWidth>().ToTable("Fittings");
            // FluentAPI alternative for [Key] attribute
            modelBuilder.Entity<ShoeWidth>().HasKey(t => t.UniqueIdent);
            // FluentAPI alternative for [Column] attribute
            modelBuilder.Entity<ShoeWidth>().Property(t => t.UniqueIdent).HasColumnName("Id");
            modelBuilder.Entity<ShoeWidth>().Property(t => t.WidthName).HasColumnName("Name");

            modelBuilder.Entity<Shoe>().Property(s => s.WidthId).HasColumnName("FittingId");
            // FluentAPI alternative for [ForeignKey] and [InverseProperty] attributes,
            // configures realtion
            // NOTE: Methods of the EntityBulder for relations:
            //       - HasOne - This method is used to start describing a relationship where the 
            //                  selected entity class has a relationship with a single object of
            //                  another type.The argument selects the navigation property, either by
            //                  name or by using a lambda expression.
            //       - HasMany - This method is used to start describing a relationship where the
            //                   selected entity class has a relationship with many objects of
            //                   another  type.The argument selects the navigation property, either
            //                   by name or by using a lambda expression.
            // NOTE: Methods for completing the relations:
            //       - WithMany - This method is used to select the inverse navigation property in a
            //                    one-to-many relationship.
            //       - WithOne - This method is used to select the inverse navigation property in a
            //                   one-to-one relationship.
            // NOTE: Method for specifying the FK and it's constraints
            //       - HasForeignKey - This method is used to select the foreign key property for
            //                         the relationship.
            //       - IsRequired - This method is used to specify whether the relationship is
            //                      required or optional.
            //       - HasConstraintName - This method is used to specify the name of the FK or PK
            //                             constraint
            modelBuilder.Entity<Shoe>()
                .HasOne(s => s.Width)
                .WithMany(w => w.Products)
                .HasForeignKey(s => s.WidthId)
                .IsRequired(true);
        }
    }
}
