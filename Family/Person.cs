using System;
using System.Collections.Generic;
using System.Linq;

namespace Family
{
    public abstract class Person
    {
        private DateTime? _deathday;

        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateTime Birthday { get; }

        public Person Spouse { get; set; }

        private readonly List<Person> _parents = new List<Person>();

        private readonly List<Person> _children = new List<Person>();
        
        private readonly List<Person> _siblings = new List<Person>();

        public Person(string firstName, string lastName, DateTime birthday)
        {
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
        }

        // public IReadOnlyList<Person> Parents { get; } = Array.Empty<Person>();

        // public IReadOnlyList<Person> Children => _children.ToArray();

        // public IReadOnlyList<Person> Siblings => _siblings.ToArray();

        public bool HasGone => _deathday.HasValue;

        public int Age => (int)((DateTime.Today - Birthday).TotalDays / 365);

        public bool IsOrphan => !IsAdult && _parents.All(p => p.HasGone);

        public bool IsAdult => Age >= 18;

        public string FullName => FirstName + " " + LastName;

        public bool IsMarried => Spouse != null;
    }

    public class Man : Person
    {
        public Man(string firstName, string lastName, DateTime birthday) 
            : base(firstName, lastName, birthday) { }
    }

    public class Woman : Person
    {
        public Woman(string firstName, string lastName, DateTime birthday) 
            : base(firstName, lastName, birthday) { }
    }
}
