using System;
using System.Collections.Generic;

namespace Family
{
    public class Man : Person
    {
        public Man(string firstName, string lastName, DateTime birthday, IEnumerable<Person> parents) 
            : base(firstName, lastName, birthday, parents) { }
    }
}
