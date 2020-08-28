using DataBase_Generator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class PaymentRepository : IRepository<Payment>
    {
        public GeneratorContext Context { get; set; }
        public PaymentRepository(GeneratorContext context)
        {
            Context = context;
        }
        public Payment Create(Payment payment)
        {
            Context.Payments.Add(payment);
            Context.SaveChanges();
            return payment;
        }

        public void CreateAll(IEnumerable<Payment> payments)
        {
            using (var context = new GeneratorContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    Context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Payments] ON");
                    Context.Payments.AddRange(payments);
                    Context.SaveChanges();
                    Context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Payments] OFF");
                    transaction.Commit();
                }
            }
        }

        public bool Delete(int id)
        {
            Payment payment = Context.Payments.Find(id);
            if (payment == null)
                return false;
            Context.Payments.Remove(payment);
            Context.SaveChanges();
            return true;
        }

        public Payment Read(int id)
        {
            return Context.Payments.Find(id);
        }

        public List<Payment> ReadAll()
        {
            return Context.Payments.ToList();
        }

        public bool Update(Payment item)
        {
            throw new NotImplementedException();
        }
    }
}
