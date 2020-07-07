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
            XML_Generarion();
  
        }
           
        static void XML_Generarion ()
        {
            List<string> Names = File.ReadAllLines("Names.txt").ToList();
            List<string> LastNames = File.ReadAllLines("LastNames.txt").ToList();
            Random rand = new Random();
            var xmlSerializerPeople = new XmlSerializer(typeof(List<Person>));
            var xmlSerializerPayments = new XmlSerializer(typeof(List<Payment>));
            var stringWriter = new StringWriter();
            List<Person> People = new List<Person>();
            List<Payment> Payments = new List<Payment>();

            for (long i = 0; i < 1000000; i++)
            {
                var person = new Person { ID = i + 1, Name = Names[rand.Next(Names.Count)], LastName = LastNames[rand.Next(LastNames.Count)] };
                People.Add(person);
            };
            
            for (long i = 0; i < 10000000; i++)
            {
                var payment = new Payment { ID = i + 1, Sum = rand.Next(1, 1000), Date = RandomDay() };
                Payments.Add(payment);
            };
           
            xmlSerializerPeople.Serialize(stringWriter, People);
            string xml = stringWriter.ToString();
            File.WriteAllText("BaseOfNames.xml", xml);
            
            xmlSerializerPayments.Serialize(stringWriter, Payments);
            File.WriteAllText("BaseOfPayments.xml", xml);

            string RandomDay()
            {
                DateTime start = new DateTime(2015, 1, 1);
                int range = (DateTime.Today - start).Days;
                return start.AddDays(rand.Next(range)).ToShortDateString();
            }
        }

        

        
    }
}
