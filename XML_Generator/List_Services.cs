﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;

namespace XML_Generator
{
    public class List_Services
    {
        
        public void CreateLists(List<Person> people, List<Payment> payments)
        {
            var Names = File.ReadAllLines(ConfigurationManager.AppSettings["NamesPath"]).ToList();
            var LastNames = File.ReadAllLines(ConfigurationManager.AppSettings["LastNamesPath"]).ToList();
            var rand = new Random();                 

            for (long i = 0; i < 10000; i++)
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

            for (long i = 0; i < 100000; i++)
            {
                var payment = new Payment
                {
                    ID = i + 1,
                    Sum = rand.Next(1, 1000),
                    Date = rand.RandomDay(),
                    PersonId = rand.Next(people.Count),
                    Person = new Person()
                };
               payments.Add(payment);
            };
        }
    }
}
