using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace XML_Generator
{
    public class List_Services
    {
        
        public void CreateLists(List<Person> people, List<Payment> payments)
        {
            var Names = File.ReadAllLines("Names.txt").ToList();
            var LastNames = File.ReadAllLines("LastNames.txt").ToList();
            var rand = new Random();                 

            for (long i = 0; i < 20; i++)
            {
                var person = new Person
                {
                    ID = i + 1,
                    Name = Names[rand.Next(Names.Count)],
                    LastName = LastNames[rand.Next(LastNames.Count)]
                };
                people.Add(person);
            };

            for (long i = 0; i < 20; i++)
            {
                var payment = new Payment
                {
                    ID = i + 1,
                    Sum = rand.Next(1, 1000),
                    Date = new DateTime().RandomDay(),
                    PersonId = rand.Next(people.Count)
                };
               payments.Add(payment);
            };
        }
    }
}
