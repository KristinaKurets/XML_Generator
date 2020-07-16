using Common.Generators;
using System;
using System.Collections.Generic;
using DataBase_Generator;
using Common;
using System.Configuration;
using XML_Generator;

namespace LINQ
{
    public class Program
    {
        static void Main(string[] args)
        {
            var listService = new ListGenerator();
            var People = new List<Person>();
            var Payments = new List<Payment>();

            listService.CreateListOfPeople(People, numOfPeople: 1000);
            listService.CreateListOfPayments(Payments, numOfPayments: 10000, numOfPeople: 1000) ;

            var xmlPeople = new XMLSerializator<List<Person>>(ConfigurationManager.AppSettings["BaseOfNames"]);
            xmlPeople.Write(People);

            var xmlPayments = new XMLSerializator<List<Payment>>(ConfigurationManager.AppSettings["BaseOfPayments"]);
            xmlPayments.Write(Payments);

            var request = new LinqRequests();
            //request.LastMonthPayments();
            //request.MaxAveragePayment();
            //request.UserSumPayments(45);
            request.Top3MaxAndMin();
        }
    }
}
