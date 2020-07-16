using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace XML_Generator
{
    public class Person
    {
        public Person()
        {
            Payments = new List<Payment>();
        }

        public long ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        [XmlIgnore]
        internal virtual List<Payment> Payments { get; set; }

    }
}
