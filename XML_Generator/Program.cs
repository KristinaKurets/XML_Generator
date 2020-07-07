using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;

namespace XML_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            //BaseOfNamesGenerator();
            PaymentGenerator();
        }
           
        static void BaseOfNamesGenerator ()
        {
            List<string> Names = File.ReadAllLines("Names.txt").ToList();
            List<string> LastNames = File.ReadAllLines("LastNames.txt").ToList();
            Random rand = new Random();
            var xmlSerializer = new XmlSerializer(typeof(List<Person>));
            var stringWriter = new StringWriter();
            List<Person> People = new List<Person>();

            for (long i = 0; i < 1000000; i++)
            {
                var person = new Person { ID = i + 1, Name = Names[rand.Next(Names.Count)], LastName = LastNames[rand.Next(LastNames.Count)] };
                People.Add(person);
            }
            
            xmlSerializer.Serialize(stringWriter, People);
            string xml = stringWriter.ToString();
            File.WriteAllText("BaseOfNames.xml", xml);
        }

        static void PaymentGenerator ()
        {
            Random rand = new Random();
            var xmlSerializer = new XmlSerializer(typeof(List<Payment>));
            var stringWriter = new StringWriter();
            List<Payment> Payments = new List<Payment>();
           
            string RandomDay()
            {
                DateTime start = new DateTime(2015, 1, 1);
                int range = (DateTime.Today - start).Days;
                return start.AddDays(rand.Next(range)).ToShortDateString();
            }

            for (long i = 0; i < 10; i++)
            {
                var payment = new Payment { ID = i + 1, Sum = rand.Next(1, 1000), Date = RandomDay() };
                Payments.Add(payment);
            };
            
            xmlSerializer.Serialize(stringWriter, Payments);
            string xml = stringWriter.ToString();
            File.WriteAllText("BaseOfPayments.xml", xml);

        }

        
    }
}
