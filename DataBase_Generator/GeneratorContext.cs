using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;
using XML_Generator;

namespace DataBase_Generator
{
    public class GeneratorContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Payment> Payments { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=LAPTOP-S2L9C420;Database=Trainee;Integrated Security=True;");
        }
    }
}
