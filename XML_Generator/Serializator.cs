using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace XML_Generator
{
    public class Serializator
    {
        public void Serialization(List<Person> People, List<Payment> Payments )
        {
            var xmlSerializerPayments = new XmlSerializer(typeof(List<Payment>));
            var xmlSerializerPeople = new XmlSerializer(typeof(List<Person>));
            var settings = new XmlWriterSettings { Indent = true };
            using (var writer = XmlWriter.Create(ConfigurationManager.AppSettings["BaseOfNames"], settings))
            {
                xmlSerializerPeople.Serialize(writer, People);
            }
            using (var writer = XmlWriter.Create(ConfigurationManager.AppSettings["BaseOfPayments"], settings))
            {
                xmlSerializerPayments.Serialize(writer, Payments);
            }

        }
        public void Deserialisation(out List<Person> NewPeople, out List<Payment> NewPayments)
        {
            var xmlSerializerPayments = new XmlSerializer(typeof(List<Payment>));
            var xmlSerializerPeople = new XmlSerializer(typeof(List<Person>));
            //var settings = new XmlWriterSettings { Indent = true };

            using (var reader = XmlReader.Create(ConfigurationManager.AppSettings["BaseOfNames"]))
            {
                NewPeople = (List<Person>)xmlSerializerPeople.Deserialize(reader);
            }
            using (var reader = XmlReader.Create(ConfigurationManager.AppSettings["BaseOfPayments"]))
            {
                NewPayments = (List<Payment>)xmlSerializerPayments.Deserialize(reader);
            }
        }

    }
}
