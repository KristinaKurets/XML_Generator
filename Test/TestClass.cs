using Common;
using DataBase_Generator;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using Repository;
using System.Collections.Generic;
using System.Linq;
using Assert = NUnit.Framework.Assert;

namespace Test
{
   public class TestClass
    {
        protected IRepository<Person> People { get; set; }
        protected IRepository<Payment> Payments { get; set; }

       public void Setup(IList<Payment> payments = null)
        {
            var mockPeople = new Mock<IRepository<Person>>();
            var mockPayments = new Mock<IRepository<Payment>>();
            mockPayments.Setup(x => x.Read(1)).Returns(new Payment { ID = 1});
            mockPayments.Setup(x => x.ReadAll()).Returns(new List<Payment>());
            mockPeople.Setup(x => x.Read(1)).Returns(new Person { ID = 1 });
            People = mockPeople.Object;
            Payments = mockPayments.Object;
        }
       

        [Test, TestCase(1)]
        public void TestReadPersonById(int id)
        {
            Setup();
            Assert.AreEqual(id, People.Read(id).ID);
        }

        [Test, TestCase(1)]
        public void ReadPaymentById(int id)
        {
            Setup();
            Assert.AreEqual(id, Payments.Read(id).ID);
        }

        [Test, TestCase(typeof(TestCases), nameof(TestCases.ReadAllTestCase))]
        public int TestCount(IList<Payment> payments)
        {
            Setup(payments);
            var result = Payments.ReadAll();
            return result.Count;
        }
    }

    public class TestCases
    {

        protected static readonly IList<Payment> payments = new List<Payment>
        {
           new Payment
           {
               ID = 1,
               Sum = 1000
           },
           new Payment
           {
               ID = 2,
               Sum = 3400
           }
        };

        public IEnumerable <TestCaseData> ReadAllTestCase
        {
            get
            {
                yield return new TestCaseData(payments).Returns(payments.Count);
                yield return new TestCaseData(null).Returns(0);
            }
        }
    }

    
}