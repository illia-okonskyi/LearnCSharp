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

            modelBuilder.Entity<SecondaryIdentity>()
                .HasOne(s => s.PrimaryIdentity)
                .WithOne(e => e.OtherIdentity)
                .HasPrincipalKey<Employee>(e => new { e.SSN, e.FirstName, e.FamilyName})
                .HasForeignKey<SecondaryIdentity>(s => new { s.PrimarySSN, s.PrimaryFirstName, s.PrimaryFamilyName});
        }
    }
}
