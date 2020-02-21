namespace FamilyTest
{
    using System;
    using Family;

    internal static class PersonData
    {
        public static (string firstName, string lastName, DateTime birthday) Ivan { get; }
            =  ("Ivan", "Ivanov", DateTime.Today.AddYears(-20));
        
        public static (string firstName, string lastName, DateTime birthday) Maria { get; }
            = ("Maria", "Petrova", DateTime.Today.AddYears(-17));

        public static Man Man
            => new Man(Ivan.firstName, Ivan.lastName, Ivan.birthday);
        
        public static Woman Woman
            => new Woman(Maria.firstName, Maria.lastName, Maria.birthday);
    }
}