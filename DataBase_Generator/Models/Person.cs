using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataBase_Generator
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
        public virtual List<Payment> Payments { get; set; }

    }
}
