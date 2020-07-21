using DataBase_Generator;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Common.Comparer
{
    public class PeopleCompare : IEqualityComparer<Person>
    {
        public bool Equals(Person x, Person y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                 return false;
            return x.ID == y.ID;
        }

        public int GetHashCode(Person person)
        {
           return person.ID.GetHashCode();
        }
    }
}
