using System;
using System.Collections.Generic;

namespace Family
{
    public class PeopleService : IPeopleService
    {
        public T Register<T>(string firstName, string lastName, DateTime birthday, IEnumerable<Person> parents) where T : Person
        {
            var person = Activator.CreateInstance(typeof(T), firstName, lastName, birthday, parents);
            return person as T;
        }
     
        public void Marry(Person who, Person with)
        {
            CheckMariage(who);
            CheckMariage(with);
            (who.Spouse, with.Spouse) = (with, who);

            static void CheckMariage(Person person)
            {
                if(person.IsMarried)
                {
                    throw new InvalidOperationException($"{person.FullName} is already married with {person.Spouse.FullName}!");   
                }
            } 
        }

        public void PassAway(Person person, DateTime date)
        {
            person.SetDeath(date);
        }
    }
}
