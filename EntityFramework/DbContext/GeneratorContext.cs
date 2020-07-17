using DataBase_Generator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace EntityFramework
{
    public class GeneratorContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-S2L9C420;Database=Trainee;Integrated Security=True;");
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<QueryResult>().HasKey(qr => new { qr.ID, qr.Name, qr.LastName, qr.TotalSum });
        }
    }
}
