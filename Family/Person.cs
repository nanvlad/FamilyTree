using System;
using System.Collections.Generic;
using System.Linq;

namespace Family
{
    public abstract class Person
    {
        private readonly DateTime _birthday;
        private DateTime? _deathday;
        private List<Person> _children = new List<Person>();
        private List<Person> _siblings = new List<Person>();

        public Person(DateTime birth, IEnumerable<Person> parents)
        {
            _birthday = birth;
            Parents = parents.ToArray();
            UpdateSiblingsRelations();
        }

        public IReadOnlyList<Person> Parents { get; } = Array.Empty<Person>();

        public IReadOnlyList<Person> Children => _children.ToArray();

        public IReadOnlyList<Person> Siblings => _siblings.ToArray();

        public bool IsDead => _deathday.HasValue;

        public int Age => (int)((DateTime.Today - _birthday).TotalDays / 365);

        public bool IsOrphan => Age < 18 && Parents.All(p => p.IsDead);

        private void UpdateSiblingsRelations()
        {
            foreach (var parent in Parents)
            {
                foreach (var child in parent._children)
                {
                    if (!child._siblings.Contains(this))
                    {
                        child._siblings.Add(this);
                    }

                    _siblings.Add(child);
                }

                parent._children.Add(this);
            }
        }
    }

    public class Man : Person
    {
        public Man(DateTime birth, IEnumerable<Person> parents) : base(birth, parents) { }
    }

    public class Woman : Person
    {
        public Woman(DateTime birth, IEnumerable<Person> parents) : base(birth, parents) { }
    }
}
