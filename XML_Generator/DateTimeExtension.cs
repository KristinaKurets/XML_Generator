using System;
using System.Collections.Generic;
using System.Text;

namespace XML_Generator
{
    public static class DateTimeExtension
    {
        public static DateTime RandomDay(this Random rand)
        {
            rand = new Random();
            var start = new DateTime(2015, 1, 1);
            var range = (DateTime.Today - start).Days;
            return start.AddDays(rand.Next(range)); ;

        }

    }
}
