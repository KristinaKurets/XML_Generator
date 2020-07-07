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
            BaseOfNamesGenerator();
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
    }
}
