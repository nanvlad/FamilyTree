using System;

namespace Family
{
    public class PeopleService : IPeopleService
    {
        public void Marry(Person who, Person with)
        {
            CheckMariage(who);
            CheckMariage(with);
            (who.Spouse, with.Spouse) = (with, who);
            
            static void CheckMariage(Person person)
            {
                if(person.IsMarried)
                {
                    throw new InvalidOperationException($"{person.FullName} is already married!");   
                }
            } 
        }

        public T Register<T>(string firstName, string lastName, DateTime? birthday) where T : Person
        {
            var person = Activator.CreateInstance(typeof(T), firstName, lastName, birthday);
            return person as T;
        }
        
        // private static void UpdateSiblingsRelations()
        // {
        //     foreach (var parent in Parents)
        //     {
        //         foreach (var child in parent._children)
        //         {
        //             if (!child._siblings.Contains(this))
        //             {
        //                 child._siblings.Add(this);
        //             }

        //             _siblings.Add(child);
        //         }

        //         parent._children.Add(this);
        //     }
        // }
    }
}
