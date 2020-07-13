using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XML_Generator
{
    public class Linq_Requests
    {
        public void LastMonthPayments()
        {
            var NewPayments = new List<Payment>();
            var NewPeople = new List<Person>();
            var xmlSerializerPayments = new XmlSerializer(typeof(List<Payment>));
            var xmlSerializerPeople = new XmlSerializer(typeof(List<Person>));
            var settings = new XmlWriterSettings { Indent = true };

            using (var reader = XmlReader.Create(ConfigurationManager.AppSettings["BaseOfNames"]))
            {
                NewPeople = (List<Person>)xmlSerializerPeople.Deserialize(reader);
            }
            using (var reader = XmlReader.Create(ConfigurationManager.AppSettings["BaseOfPayments"]))
            {
                NewPayments = (List<Payment>)xmlSerializerPayments.Deserialize(reader);
            }

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
            var NewPayments = new List<Payment>();
            var NewPeople = new List<Person>();
            var xmlSerializerPayments = new XmlSerializer(typeof(List<Payment>));
            var xmlSerializerPeople = new XmlSerializer(typeof(List<Person>));
            var settings = new XmlWriterSettings { Indent = true };

            using (var reader = XmlReader.Create(ConfigurationManager.AppSettings["BaseOfNames"]))
            {
                NewPeople = (List<Person>)xmlSerializerPeople.Deserialize(reader);
            }
            using (var reader = XmlReader.Create(ConfigurationManager.AppSettings["BaseOfPayments"]))
            {
                NewPayments = (List<Payment>)xmlSerializerPayments.Deserialize(reader);
            }
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
    }
}
