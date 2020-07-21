using DataBase_Generator;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Common.Comparer
{
    public class PaymentsCompare : IEqualityComparer<Payment>
    {
        public bool Equals(Payment x, Payment y)
        {
            if (x == null && y == null)
                return true;
            else if (x == null || y == null)
                return false;
            return x.ID == y.ID;
        }

        public int GetHashCode(Payment payment)
        {
            return payment.ID.GetHashCode();
        }
    }
}
