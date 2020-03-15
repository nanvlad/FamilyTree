using System;
using System.Collections.Generic;

namespace Family
{
    public class Woman : Person
    {
        public Woman(string firstName, string lastName, DateTime birthday, IEnumerable<Person> parents) 
            : base(firstName, lastName, birthday, parents) { }
    }
}
