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
using System.Configuration;
using DataBase_Generator;
using Microsoft.EntityFrameworkCore;

namespace XML_Generator
{
    class Program
    {
       static void Main(string[] args)
       {
            var service = new List_Services();
            var xml_data = new Serializator();
            var People = new List<Person>();
            var Payments = new List<Payment>();
            service.CreateLists(People, Payments, 10000, 100000);
            xml_data.Serialization(People, Payments);
            
            ////Json serializing:
            //var jsonPeople = JsonConvert.SerializeObject(People, Newtonsoft.Json.Formatting.Indented);
            //File.WriteAllText("BaseOfNames.json", jsonPeople);
            //var jsonPayments = JsonConvert.SerializeObject(Payments, Newtonsoft.Json.Formatting.Indented);
            //File.WriteAllText("BaseOfPayments.json", jsonPayments);


            //LINQ
            var request = new Linq_Requests();
            //request.LastMonthPayments();
            //request.MaxAveragePayment();
            request.UserSumPayments(400);
            //request.Top3MaxAndMin();


            ////dbContext
            //var context = new GeneratorContext();
            //var paymentsResult = context.Payments.FromSqlRaw("PaymentsFromXML").ToList();
            //var peopleResult = context.People.FromSqlRaw("PeopleFromXML").ToList();
            //foreach (var item in peopleResult)
            //{
            //    Console.WriteLine($"{item.Name} {item.LastName}");
            //}
        }
    }
}