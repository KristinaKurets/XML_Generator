using DataBase_Generator;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
    public class PersonRepository : IRepository<Person>
    {
        public GeneratorContext Context { get; set; }
        public PersonRepository(GeneratorContext context)
        {
            Context = context;
        }
        public Person Create(Person person)
        {
            Context.People.Add(person);
            Context.SaveChanges();
            return person;
        }

        public void CreateAll(IEnumerable<Person> people)
        {
            using (var context = new GeneratorContext())
            {
                using (var transaction = context.Database.BeginTransaction())
                {
                    Context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[People] ON");
                    Context.People.AddRange(people);
                    Context.SaveChanges();
                    Context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT [dbo].[People] OFF");
                    transaction.Commit();
                }
            }
        }
        public bool Delete(int id)
        {
            Person person = Context.People.Find(id);
            if (person == null)
                return false;
            Context.People.Remove(person);
            Context.SaveChanges();
            return true;
        }

        public Person Read(int id)
        {
            return Context.People.Find(id);
        }

        public IQueryable<Person> ReadAll()
        {
            return Context.People;
        }

        public bool Update(Person item)
        {
            throw new NotImplementedException();
        }
    }
}
