using System;
using Microsoft.EntityFrameworkCore;

namespace AdvancedApp.Models
{
    public class AdvancedContext : DbContext
    {
        public AdvancedContext(DbContextOptions<AdvancedContext> options)
            : base(options)
        {
            // Configure default change-tracking behavior for all the requests
            //ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;


        }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // NOTE: The Hi-Lo strategy is an optimization that allows Entity Framework Core to
            //       create primary key values, instead of the database server, while still ensuring
            //       those values are unique. The idea is to get from the DB server the next LO 
            //       number of the sequence and EF Core can store up to N values before it reaches
            //       the HI number of the sequence: LO + N = HI. After that EF Core obtains the next
            //       LO number of the sequence again and process of the insertion continues.
            //       This feature is available only for MS SQL Server. Alternative and default key
            //       generation strategy is Identity when EF Core examines DB every time new value
            //       is inserted to obtain it's unique key (primary key)
            //modelBuilder.Entity<Employee>().Property(e => e.Id).ForSqlServerUseSequenceHiLo();


            // NOTE: A query filter is applied to all of the queries made in the application for a
            //       specific entity class. One useful application of the query filter is to
            //       implement a “soft delete” feature that marks objects that are deleted without
            //       removing them from the database, allowing data to be restored if it has been
            //       deleted by mistake.
            modelBuilder.Entity<Employee>().HasQueryFilter(e => !e.SoftDeleted);

            modelBuilder.Entity<Employee>().Ignore(e => e.Id);
            modelBuilder.Entity<Employee>().HasKey(e => new { e.SSN, e.FirstName, e.FamilyName });

            // Attribute alternative is [Column(TypeName = "decimal(8, 2)")]
            // For backing fileds there is no attribute alternative
            // NOTE: PropertyAccessMode enum values:
            //       - FieldDuringConstruction - This is the default behavior and tells Entity
            //                                   Framework Core to use the backing field when first
            //                                   creating the object and then to use the property
            //                                   for all other operations, including change
            //                                   detection.
            //       - Field - This value tells Entity Framework Core to ignore the property and
            //                 always use the backing field.
            //       - Property - This value tells Entity Framework Core to always use the property
            //                    and ignore the backing field.
            // The [ConcurrencyCheck] attribute is used as alternative the FluentAPI method
            // IsConcurrencyToken()
            modelBuilder.Entity<Employee>().Property(e => e.Salary)
                .HasColumnType("decimal(8,2)")
                .HasField("databaseSalary")
                .UsePropertyAccessMode(PropertyAccessMode.Field);
            
            // Declared shadow property. No attribute alternative is available
            modelBuilder.Entity<Employee>().Property<DateTime>("LastUpdated")
                .HasDefaultValue(new DateTime(2000, 1, 1));

            // NOTE: Sequence configuration methods
            //       - StartsAt - This method is used to specify the starting value for the
            //                    sequence.
            //       - IncrementsBy - This method is used to specify the amount by which the
            //                        sequence is incremented after a value is generated.
            //       - IsCyclic - This method is used to specify whether the sequence starts over
            //                    when the maximum value is reached.
            //       - HasMax - This method is used to specify a maximum value for the sequence.
            //       - HasMin - This method is used to specify a minimum value for the sequence.
            modelBuilder.HasSequence<int>("ReferenceSequence")
                .StartsAt(100)
                .IncrementsBy(2);
            // Default values can be genereted by the database server. No attribute alternative is
            // available
            //modelBuilder.Entity<Employee>().Property(e => e.GeneratedValue)
            //    .HasDefaultValueSql(@"'REFERENCE_'+ CONVERT(varchar, NEXT VALUE FOR ReferenceSequence)");

            // Computed columns are configured using the HasComputedColumnSql method, which
            // receives a SQL expression that will be used to generate the property values.
            // No attribute alternative is avilable
            modelBuilder.Entity<Employee>().Property(e => e.GeneratedValue)
                .HasComputedColumnSql(@"SUBSTRING(FirstName, 1, 1) + FamilyName PERSISTED");
            modelBuilder.Entity<Employee>().HasIndex(e => e.GeneratedValue);

            // Attribute alternative is [TimeStamp] attribute. Row version property must be byte[]
            // type to avoid formatting issues
            // NOTE: RowVersion data type can not be included table-valued functions
            //modelBuilder.Entity<Employee>().Property(e => e.RowVersion).IsRowVersion();
            modelBuilder.Entity<Employee>().Ignore(e => e.RowVersion);

            // The OnDelete() FluentAPI method configures delete behavior for entity.
            // There is no attribute alternative
            // NOTE: The DeleteBehavior Values
            //       - Cascade - The dependent entity is deleted automatically along with the
            //                   principal entity.
            //       - SetNull - The primary key of the dependent entity is set to null by the
            //                   database server (if this feature is supported).
            //       - ClientSetNull - The primary key of the dependent entity is set to null by
            //                         EF Core. Target entities must be loaded by the EF Core
            //       - Restrict - No change is made to the dependent entity.
            modelBuilder.Entity<SecondaryIdentity>()
                .HasOne(s => s.PrimaryIdentity)
                .WithOne(e => e.OtherIdentity)
                .HasPrincipalKey<Employee>(e => new { e.SSN, e.FirstName, e.FamilyName})
                .HasForeignKey<SecondaryIdentity>(s => new { s.PrimarySSN, s.PrimaryFirstName, s.PrimaryFamilyName})
                .OnDelete(DeleteBehavior.Restrict);

            // Attribute alternative is [MaxLength]
            modelBuilder.Entity<SecondaryIdentity>().Property(e => e.Name).HasMaxLength(100);
        }
    }
}
