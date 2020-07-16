using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace DataBase_Generator
{
    public class Payment
    {
        public long ID { get; set; }
        public int Sum { get; set; }
        public DateTime Date { get; set; }
        public long PersonId { get; set; }
        
        [XmlIgnore]
        public Person Person { get; set; }

    }
}
