using System;

namespace AdvancedApp.Models
{
    public class Employee
    {
        // backing field
        private decimal databaseSalary;

        public long Id { get; set; }
        public string SSN { get; set; }
        public string FirstName { get; set; }
        public string FamilyName { get; set; }
        public decimal Salary
        {
            get => databaseSalary;
            // NOTE: for setters who don't set backing fields every time they are called:
            //       backing fileds is initialized with correct db value only when it's directly
            //       loaded by previos query. E.g. setter saves only % 2 == 0 values, so for storing
            //       correct value intstead of default initialized 0 the load query is requiered.
            set => databaseSalary = Math.Max(0, value);
        }

        public SecondaryIdentity OtherIdentity { get; set; }

        public bool SoftDeleted { get; set; } = false;

        public DateTime LastUpdated { get; set; }
        public byte[] RowVersion { get; set; }

        public string GeneratedValue { get; set; }
    }
}
