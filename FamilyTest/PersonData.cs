namespace FamilyTest
{
    using System;
    using System.Collections.Generic;
    using Family;

    internal static class PersonData
    {
        public static (string firstName, string lastName, DateTime birthday) Ivan { get; }
            =  ("Ivan", "Ivanov", DateTime.Today.AddYears(-20));
        
        public static (string firstName, string lastName, DateTime birthday) Maria { get; }
            = ("Maria", "Petrova", DateTime.Today.AddYears(-17));
        
        public static (string firstName, string lastName, DateTime birthday) Petro { get; }
            = ("Petro", "Ivanov", DateTime.Today);

        public static (string firstName, string lastName, DateTime birthday) Sveta { get; }
            = ("Sveta", "Petrova", DateTime.Today);

        public static Man Man
            => new Man(Ivan.firstName, Ivan.lastName, Ivan.birthday, null);
        
        public static Woman Woman
            => new Woman(Maria.firstName, Maria.lastName, Maria.birthday, null);

        public static Man GetBoy(IEnumerable<Person> parents)
        {
            return new Man(Petro.firstName, Petro.lastName, Petro.birthday, parents);
        }
        

        public static Woman GetGirl(IEnumerable<Person> parents)
        {
            return new Woman(Sveta.firstName, Sveta.lastName, Sveta.birthday, parents);
        }
    }
}