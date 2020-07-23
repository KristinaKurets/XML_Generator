using Common;
using Common.Comparer;
using DataBase_Generator;
using Microsoft.EntityFrameworkCore;
using Quartz;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsService.Jobs
{
    public class ItemsAdder : IJob
    {
        public async Task Execute(IJobExecutionContext context)
        {
            await Task.Run(() =>
            {
                var people = new XMLDeserializator<List<Person>>();
                var payments = new XMLDeserializator<List<Payment>>();
                var tablePeople = people.Load(ConfigurationManager.AppSettings["BaseOfNames"]);
                var tablePayments = payments.Load(ConfigurationManager.AppSettings["BaseOfPayments"]);
                var peopleComparer = new PeopleCompare();
                var paymentsComparer = new PaymentsCompare();
               
                if (tablePeople?.Any() == true)
                {
                    using (var dbContext = new GeneratorContext())
                    {
                        using (var transaction = dbContext.Database.BeginTransaction())
                        {
                            dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[People] ON");
                            var clients = dbContext.People.ToList();
                            var difClients = tablePeople.Except(clients, peopleComparer);
                            dbContext.AddRange(difClients);
                            dbContext.SaveChanges();
                            dbContext.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[People] OFF");
                            transaction.Commit();
                        }
                    }
                }

                if (tablePayments?.Any() == true)
                {
                    using (var context = new GeneratorContext())
                    {
                        using (var transaction = context.Database.BeginTransaction())
                        {
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Payments] ON");
                            var clientPayments = context.Payments.ToList();
                            var difPayments = tablePayments.Except(clientPayments, paymentsComparer);
                            context.AddRange(difPayments);
                            context.SaveChanges();
                            context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[Payments] OFF");
                            transaction.Commit();
                        }
                    }
                }
            });
            
        }
    }
}
