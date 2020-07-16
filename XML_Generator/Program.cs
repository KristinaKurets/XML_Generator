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

            ////dbContext
            var context = new GeneratorContext();
            var paymentsResult = context.Payments.FromSqlRaw("PaymentsFromXML").ToList();
            var peopleResult = context.People.FromSqlRaw("PeopleFromXML").ToList();

            foreach (var item in peopleResult)
            {
                Console.WriteLine($"{item.Name} {item.LastName}");
            }

        }
    }
}