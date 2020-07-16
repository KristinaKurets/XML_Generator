using DataBase_Generator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramework
{
    public class EFService
    {
        private GeneratorContext context;
        public EFService (GeneratorContext context)
        {
            this.context = context;
        }

        public IQueryable<QueryResult> LastMonthPayments()
        {
            return context.Set<QueryResult>().FromSqlRaw("LastMonthPayments");
        }
        public IQueryable<QueryResult> MaxAveragePayment()
        {
            return context.Set<QueryResult>().FromSqlRaw("MaxAveragePayment");
        }

        public IQueryable<QueryResult> UserSumPayments(long id)
        {
            return context.Set<QueryResult>().FromSqlRaw("UserSumPayments" , id);
        }
        public IQueryable<QueryResult> Top3MaxAndMin()
        {
            return context.Set<QueryResult>().FromSqlRaw("Top3MaxAndMin");
        }

        

    }
}
