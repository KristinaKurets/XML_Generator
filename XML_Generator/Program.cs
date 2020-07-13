﻿using Microsoft.EntityFrameworkCore.Internal;
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
        [Obsolete]
        static void Main(string[] args)
        {
            var service = new List_Services();
            var People = new List<Person>();
            var Payments = new List<Payment>();
            service.CreateLists(People, Payments);

            //XML serializing:
            var xmlSerializerPayments = new XmlSerializer(typeof(List<Payment>));
            var xmlSerializerPeople = new XmlSerializer(typeof(List<Person>));
            var settings = new XmlWriterSettings { Indent = true };
            //using (var writer = XmlWriter.Create(ConfigurationManager.AppSettings["BaseOfNames"], settings))
            //{
            //    xmlSerializerPeople.Serialize(writer, People);
            //}
            //using (var writer = XmlWriter.Create(ConfigurationManager.AppSettings["BaseOfPayments"], settings))
            //{
            //    xmlSerializerPayments.Serialize(writer, Payments);
            //}

            ////Json serializing:
            //var jsonPeople = JsonConvert.SerializeObject(People, Newtonsoft.Json.Formatting.Indented);
            //File.WriteAllText("BaseOfNames.json", jsonPeople);
            //var jsonPayments = JsonConvert.SerializeObject(Payments, Newtonsoft.Json.Formatting.Indented);
            //File.WriteAllText("BaseOfPayments.json", jsonPayments);


            //LINQ
            var request = new Linq_Requests();
            //request.LastMonthPayments();
            //request.MaxAveragePayment();
            request.UserSumPayments(4);


            ////dbContext
            //var context = new GeneratorContext();
            //var paymentsResult = context.Payments.FromSql("PaymentsFromXML").ToList();
            //var peopleResult = context.People.FromSql("PeopleFromXML").ToList();
            //foreach (var item in peopleResult)
            //{
            //    Console.WriteLine($"{item.Name} {item.LastName}");
            //}
        }
    }
}