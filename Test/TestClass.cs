using Common;
using DataBase_Generator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Repository;
using System.Collections.Generic;
using System.Linq;
using Test.Extensions;
using Test.Resources;
using Assert = NUnit.Framework.Assert;

namespace Test
{
    [TestClass]
    public class TestClass
    {
        private List<Person> People { get; set; }
        private List<Payment> Payments { get; set; }
        
        [TestInitialize]
        public void Setup()
        {
            var desPeople = new XMLDeserializator<List<Person>>();
            var desPayments = new XMLDeserializator<List<Payment>>();
            People = desPeople.Load(DataResources.PeoplePath);
            Payments = desPayments.Load(DataResources.PaymentsPath);
        }

        [TestMethod]
        public void Test1()
        {
            var mockContext = new Mock<GeneratorContext>();
            var peopleDbSet = People.GetQueryableMockDbSet();
            foreach (var person in People)
            {
                peopleDbSet.Add(person);
            }
            mockContext.Setup(context => context.People).Returns(peopleDbSet);
            var dbContext = mockContext.Object;
            var mockRepository = new Mock<IRepository<Person>>();
            mockRepository.Setup(e => e.ReadAll()).Returns(dbContext.People);
            var peopleRepository = mockRepository.Object;
            var records = peopleRepository.ReadAll();

            Assert.IsTrue(People.Count == 1000);
        }

        [TestMethod]
        public void Test2()
        {
            var mockContext = new Mock<GeneratorContext>();
            var paymentsDbSet = Payments.GetQueryableMockDbSet();
            foreach (var person in Payments)
            {
                paymentsDbSet.Add(person);
            }
            mockContext.Setup(context => context.Payments).Returns(paymentsDbSet);
            var dbContext = mockContext.Object;
            var mockRepository = new Mock<IRepository<Payment>>();
            mockRepository.Setup(e => e.ReadAll()).Returns(dbContext.Payments);
            var paymentsRepository = mockRepository.Object;
            var records = paymentsRepository.ReadAll();

            Assert.IsTrue(Payments.Count == 10000);
        }
    }
}