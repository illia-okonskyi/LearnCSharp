using Microsoft.EntityFrameworkCore;

namespace AdvancedApp.Models
{
    public class AdvancedContext : DbContext
    {
        public AdvancedContext(DbContextOptions<AdvancedContext> options)
            : base(options) { }

        public DbSet<Employee> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Snippet 1
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

            // Snippet 2
            //modelBuilder.Entity<Employee>().HasIndex(e => e.SSN).HasName("SSNIndex").IsUnique();

            modelBuilder.Entity<Employee>().Ignore(e => e.Id);
            modelBuilder.Entity<Employee>().HasKey(e => e.SSN);
            // Snippet 3
            //modelBuilder.Entity<Employee>().HasAlternateKey(e => e.SSN);

            modelBuilder.Entity<SecondaryIdentity>()
                .HasOne(s => s.PrimaryIdentity)
                .WithOne(e => e.OtherIdentity)
                // Snippet 3
                //.HasPrincipalKey<Employee>(e => e.SSN)
                .HasForeignKey<SecondaryIdentity>(s => s.PrimarySSN);
        }
    }
}
