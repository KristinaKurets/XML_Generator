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
<<<<<<< HEAD
           //master commit
            var Names = File.ReadAllLines("Names.txt").ToList();
            var LastNames = File.ReadAllLines("LastNames.txt").ToList();
            var rand = new Random();
=======
            var service = new List_Services();
>>>>>>> e6674f8f011904329f94f0d5f7868c4064b62597
            var People = new List<Person>();
            var Payments = new List<Payment>();
            service.CreateLists(People, Payments);
            
            

            ////XML serializing:
            //var xmlSerializerPayments = new XmlSerializer(typeof(List<Payment>));
            //var xmlSerializerPeople = new XmlSerializer(typeof(List<Person>));
            //XmlWriterSettings settings = new XmlWriterSettings { Indent = true };
            //using (var writer = XmlWriter.Create("BaseOfNames.xml", settings))
            //{
            //    xmlSerializerPeople.Serialize(writer, People);
            //}
            //using (var writer = XmlWriter.Create("BaseOfPayments.xml", settings))
            //{
            //    xmlSerializerPayments.Serialize(writer, Payments);
            //}

            //Json serializing:
            var jsonPeople = JsonConvert.SerializeObject(People, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("BaseOfNames.json", jsonPeople);
            var jsonPayments = JsonConvert.SerializeObject(Payments, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText("BaseOfPayments.json", jsonPayments);


            ////LINQ
            //XDocument xdocPeople = XDocument.Load("BaseOfNames.xml");
            //var items = from xe in xdocPeople.Element("ArrayOfPerson").Elements("Person")
            //            where xe.Element("Name").Value == "Toby"
            //            select new Person
            //            {
            //                Name = xe.Element("Name").Value,
            //                LastName = xe.Element("LastName").Value,

            //            };


            //XDocument xdocPayments = XDocument.Load("BaseOfPayments.xml");
            //var today = DateTime.Today;
            //var currMonth = today.Month;
            //var prevMonth = today.AddMonths(-3);

            //Console.WriteLine("People who have had payments for the last month(June 2020)");

            //var firstTask =
            //    xdocPayments.Element("ArrayOfPayment").Elements("Payment")
            //.Where(x => DateTime.Parse(x.Element("Date").Value) == prevMonth)
            //.Select(xe => new Payment
            //{
            //    PersonId = long.Parse(xe.Element("PersonId").Value)
            //})
            //    .GroupBy(payment => payment.PersonId)
            //    .Select(x => new
            //    {
            //        PersonId = x.Key,
            //        Sum = x.Sum(c => c.Sum)
            //    });


            //var secondTask =
            //    xdocPayments.Element("ArrayOfPayment").Elements("Payment")
            //    .Where(x => DateTime.Parse(x.Element("Date").Value) >= today.AddMonths(-6) && DateTime.Parse(x.Element("Date").Value) <= today);
                

        }

    }
    public static class DateTimeExtension
    {
        public static DateTime RandomDay(this DateTime start)
        {
            var rand = new Random();
            start = new DateTime(2015, 1, 1);
            var range = (DateTime.Today - start).Days;
            return start.AddDays(rand.Next(range)); ;
 
        }
    }
}
