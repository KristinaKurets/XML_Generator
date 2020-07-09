using System;
using System.Collections.Generic;
using XML_Generator;

namespace DataBase_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new List_Services();
            var People = new List<Person>();
            var Payments = new List<Payment>();
            service.CreateLists(People, Payments);

            foreach (var person in People)
            {
                Console.WriteLine($"{person.Name} {person.LastName}");
            }
            using (var context = new GeneratorContext())
            {
                context.People.AddRange(People);
                context.Payments.AddRange(Payments);
                context.SaveChangesAsync();
            }

            //check
        }
    }
}
