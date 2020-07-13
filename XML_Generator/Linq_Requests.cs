using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XML_Generator
{
    public class Linq_Requests
    {
        public void Deserialisation(out List<Person> NewPeople,out List<Payment> NewPayments)
        {
            var xmlSerializerPayments = new XmlSerializer(typeof(List<Payment>));
            var xmlSerializerPeople = new XmlSerializer(typeof(List<Person>));
            //var settings = new XmlWriterSettings { Indent = true };
            var path = ConfigurationManager.AppSettings["BaseOfNames"];
            using (var reader = XmlReader.Create(ConfigurationManager.AppSettings["BaseOfNames"]))
            {
                NewPeople = (List<Person>)xmlSerializerPeople.Deserialize(reader);
            }
            using (var reader = XmlReader.Create(ConfigurationManager.AppSettings["BaseOfPayments"]))
            {
                NewPayments = (List<Payment>)xmlSerializerPayments.Deserialize(reader);
            }
        }
        public void LastMonthPayments()
        {
            var NewPeople = new List<Person>();
            var NewPayments = new List<Payment>();
            //Deserialisation(NewPeople, NewPayments);
            // Вывести людей у которых за последний месяц были платежи (также вывести сумму платежей каждого из этих людей):
            var firstTask = NewPeople.Join(
                NewPayments,
                person => person.ID,
                payment => payment.PersonId,
                (person, payment) => new
                {
                    Name = person.Name,
                    LastName = person.LastName,
                    Date = payment.Date,
                    Sum = payment.Sum,
                    Amount = NewPayments.Sum(ex => ex.PersonId == person.ID ? payment.Sum : 0)
                }
                    ).Where(x => x.Date > DateTime.Now.AddDays(-30))
                      .Select(xe => new
                      {
                          Name = xe.Name,
                          LastName = xe.LastName,
                          Sum = xe.Amount,
                      });

            if (firstTask.ToList().Count == 0)
            {
                Console.WriteLine(myRes.NoPeople);
            }
            else
            {
                Console.WriteLine(myRes.PeopleFromFirstTask);
                foreach (var item in firstTask)
                {
                    Console.WriteLine($"Last month {item.Name} {item.LastName} paid {item.Sum}$");
                }
            }

        }
        public void MaxAveragePayment()
        {
            var NewPeople = new List<Person>();
            var NewPayments = new List<Payment>();
            Deserialisation(out NewPeople, out NewPayments);
            //Вывести 5 людей, у которых за последние 6 месяцев был наибольший средний платеж:
            var secondTask = NewPeople.Join(
                NewPayments,
                person => person.ID,
                payment => payment.PersonId,
                (person, payment) => new
                {
                    Name = person.Name,
                    LastName = person.LastName,
                    Date = payment.Date,
                    Sum = payment.Sum,

                }
                    ).Where(x => x.Date > DateTime.Now.AddMonths(-6))
                    .GroupBy(n => new { Name = n.Name, LastName = n.LastName })
                    .Select(xe => new
                    {
                        Name = xe.Key.Name,
                        LastName = xe.Key.LastName,
                        Sum = xe.Average(x => x.Sum)
                    })
                    .OrderByDescending(x => x.Sum).Take(5);

            Console.WriteLine(myRes.PeopleFromSecondTask);
            foreach (var item in secondTask)
            {
                Console.WriteLine($"{item.Name} {item.LastName} - average sum for last 6 month is {item.Sum}$");
            }

        }
        public void UserSumPayments(long userID)
        {
            var NewPeople = new List<Person>();
            var NewPayments = new List<Payment>();
            Deserialisation(out NewPeople, out NewPayments);
            NewPeople = NewPeople.Where(p => p.ID == userID).ToList();
            NewPayments = NewPayments.Where(p => p.PersonId == userID).ToList();
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();

            //Вывести сумму платежей по конкретному человеку
           var thirdTask = NewPeople.Join(
                NewPayments,
                person => person.ID,
                payment => payment.PersonId,
                (person, payment) => new
                {
                    ID = person.ID,
                    Name = person.Name,
                    LastName = person.LastName,
                    Date = payment.Date,
                    Sum = payment.Sum,

                })
                    .GroupBy(n => n.ID)
                    .Select(xe => new
                    {
                        ID = xe.Key,
                        Name = xe.First().Name,
                        LastName = xe.First().LastName,
                        Sum = xe.Sum(x => x.Sum)
                    })
                    .FirstOrDefault();

            Console.WriteLine($"{thirdTask.Name} {thirdTask.LastName} - {thirdTask.Sum}");
            stopWatch.Stop();
        }
    }
}
