using System.Collections.Generic;

namespace ModelBinding.Models
{
    public interface IRepository
    {
        IEnumerable<Person> People { get; }
        Person this[int id] { get; set; }
    }

    public class MemoryRepository : IRepository
    {
        private Dictionary<int, Person> _people = new Dictionary<int, Person>
        {
            [1] = new Person
            {
                Id = 1,
                FirstName = "Bob",
                LastName = "Smith",
                Role = Role.Admin
            },
            [2] = new Person
            {
                Id = 2,
                FirstName = "Anne",
                LastName = "Douglas",
                Role = Role.User
            },
            [3] = new Person
            {
                Id = 3,
                FirstName = "Joe",
                LastName = "Able",
                Role = Role.User
            },
            [4] = new Person
            {
                Id = 4,
                FirstName = "Mary",
                LastName = "Peters",
                Role = Role.Guest
            }
        };

        public IEnumerable<Person> People => _people.Values;

        public Person this[int id]
        {
            get => _people.ContainsKey(id) ? _people[id] : null;
            set => _people[id] = value;
        }
    }
}
