namespace Family
{
    using System;
    using System.Collections.Generic;

    public interface IPeopleService
    {
        T Register<T>(string firstName, string lastName, DateTime birthday, IEnumerable<Person> parents) where T : Person;

        void Marry(Person who, Person with);

        void PassAway(Person person, DateTime at);
    }
}