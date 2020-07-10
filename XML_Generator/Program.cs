using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace XML_Generator
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new List_Services();
            var People = new List<Person>();
            var Payments = new List<Payment>();
            service.CreateLists(People, Payments);

            //XML serializing:
            var xmlSerializerPayments = new XmlSerializer(typeof(List<Payment>));
            var xmlSerializerPeople = new XmlSerializer(typeof(List<Person>));
            //var settings = new XmlWriterSettings { Indent = true };
            //using (var writer = XmlWriter.Create("BaseOfNames.xml", settings))
            //{
            //    xmlSerializerPeople.Serialize(writer, People);
            //}
            //using (var writer = XmlWriter.Create("BaseOfPayments.xml", settings))
            //{
            //    xmlSerializerPayments.Serialize(writer, Payments);
            //}

            ////Json serializing:
            //var jsonPeople = JsonConvert.SerializeObject(People, Newtonsoft.Json.Formatting.Indented);
            //File.WriteAllText("BaseOfNames.json", jsonPeople);
            //var jsonPayments = JsonConvert.SerializeObject(Payments, Newtonsoft.Json.Formatting.Indented);
            //File.WriteAllText("BaseOfPayments.json", jsonPayments);


            //LINQ
            var NewPayments = new List<Payment>();
            var NewPeople = new List<Person>();

            using (var reader = XmlReader.Create("BaseOfNames.xml"))
            {
                NewPeople = (List<Person>)xmlSerializerPeople.Deserialize(reader);
            }
            using (var reader = XmlReader.Create("BaseOfPayments.xml"))
            {
                NewPayments = (List<Payment>)xmlSerializerPayments.Deserialize(reader);
            }

            var firstTask = NewPeople.Join(
                NewPayments,
                person => person.ID,
                payment => payment.PersonId,
                (person, payment) => new {
                    Name = person.Name,
                    LastName = person.LastName,
                    Date = payment.Date,
                    Sum = payment.Sum
                 }
                    ).Where(x => x.Date > DateTime.Now.AddDays(-30))
                      .Select(xe => new 
                       {
                        Name = xe.Name,
                        LastName = xe.LastName,
                        Sum = xe.Sum,
                        Date = xe.Date
                       });

            if (firstTask.ToList().Count == 0)
            {
                Console.WriteLine("There are no people who have had payments for the last month.");
            }
            else
            {
                Console.WriteLine("People who have had payments for the last month:");
                foreach (var item in firstTask)
                {
                    Console.WriteLine($"{item.Name} {item.LastName} paid {item.Sum}$ - {item.Date}");
                }
            }

            //.GroupBy(payment => payment.PersonId)
            //.Select(x => new
            //{
            //    PersonId = x.Key,
            //    Sum = x.Key
            //});



            //var secondTask =
            //    xdocPayments.Element("ArrayOfPayment").Elements("Payment")
            //    .Where(x => DateTime.Parse(x.Element("Date").Value) >= today.AddMonths(-6) && DateTime.Parse(x.Element("Date").Value) <= today);



        }
    }
}