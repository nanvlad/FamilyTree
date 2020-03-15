using System;
using System.Collections.Generic;
using System.Linq;

namespace Family
{
    public abstract class Person : IEquatable<Person>
    {
        private readonly List<Person> _parents = new List<Person>();
        private readonly List<Person> _children = new List<Person>();

        public Person(string firstName, string lastName, DateTime birthday, IEnumerable<Person> parents)
        {
            (FirstName, LastName, Birthday) = (firstName, lastName, birthday);
            AddParentsWithChildren(parents);
        }
        
        public string FirstName { get; private set; }

        public string LastName { get; private set; }

        public DateTime Birthday { get; }

        public Person Spouse { get; set; }

        public DateTime? DeathDate{ get; private set; }

        public IReadOnlyList<Person> Parents => _parents.ToArray();

        public IReadOnlyList<Person> Children => _children.ToArray();

        public bool HasGone => DeathDate.HasValue;

        public int Age => (int)((DateTime.Today - Birthday).TotalDays / 365);

        public bool IsOrphan => !IsAdult && _parents.All(p => p.HasGone);

        public bool IsAdult => Age >= 18;

        public string FullName => FirstName + " " + LastName;

        public bool IsMarried => Spouse != null;

        public void SetDeath(DateTime date) => DeathDate = date;

        public bool Equals(Person other)
            => (this?.FullName, this?.Birthday) == (other?.FullName, other?.Birthday);

        public override bool Equals(object obj)
            => Equals(obj as Person);

        public override int GetHashCode() 
            => (this?.FullName, this?.Birthday).GetHashCode();

        private void AddParentsWithChildren(IEnumerable<Person> parents)
        {
            if(parents != null)
            {
                foreach(var parent in parents)
                {
                    AddChild(parent);
                    _parents.Add(parent);
                }
            }

            void AddChild(Person parent)
            {
                if(!parent._children.Contains(this))
                {
                    parent._children.Add(this);
                }
            }
        }
    }
}
