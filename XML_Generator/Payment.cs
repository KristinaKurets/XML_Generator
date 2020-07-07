using System;
using System.Collections.Generic;
using System.Text;

namespace XML_Generator
{
    public class Payment
    {
        public long ID { get; set; }
        public int Sum { get; set; }
        public string Date { get; set; }
        public long PersonId { get; set; }
    }
}
