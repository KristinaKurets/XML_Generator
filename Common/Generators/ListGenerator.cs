using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using DataBase_Generator;


namespace Common.Generators
{
    public class ListGenerator
    {
        public void CreateListOfPeople(List<Person> people, long numOfPeople)
        {
            var Names = File.ReadAllLines(ConfigurationManager.AppSettings["NamesPath"]).ToList();
            var LastNames = File.ReadAllLines(ConfigurationManager.AppSettings["LastNamesPath"]).ToList();
            var rand = new Random();

            for (long i = 0; i < numOfPeople; i++)
            {
                var person = new Person
                {
                    ID = i + 1,
                    Name = Names[rand.Next(Names.Count)],
                    LastName = LastNames[rand.Next(LastNames.Count)],
                    Payments = new List<Payment>()
                };
                people.Add(person);
            };

        }
        public void CreateListOfPayments(List<Payment> payments, long numOfPayments, int numOfPeople)
        {
            var rand = new Random();
            for (long i = 0; i < numOfPayments; i++)
            {
                var payment = new Payment
                {
                    ID = i + 1,
                    Sum = rand.Next(1, 1000),
                    Date = rand.RandomDay(),
                    PersonId = rand.Next(numOfPeople),
                    Person = new Person()
                };
                payments.Add(payment);
            };
        }

    }
}
