using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zee.Sample.CaveatEmptor.Model;
using NHibernate;

namespace UnitTest
{
    [TestClass]
    public class Test_Retrieving
    {
        private ISessionFactory _sessionFactory;
        private NHibernate.Cfg.Configuration _configuration;

        public Test_Retrieving()
        {
            _configuration = new NHibernate.Cfg.Configuration();
            _configuration.Configure();
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        [TestCategory("Fetching Strategy")]
        [TestMethod]
        public void Can_Get()
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                session.BeginTransaction();

                Category books = session.Get<Category>(14L);
                Assert.AreEqual("Books", books.Name);

                Category magazine = session.Load<Category>(15L);
                Assert.AreEqual("Magazine", magazine.Name);
                session.Transaction.Commit();
            }
        }

        [TestCategory("Fetching Strategy")]
        [TestMethod]
        public void Can_Get_From_Cache()
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                //session.BeginTransaction();
                Category books = session.Get<Category>(14L);
                Assert.AreEqual("Books", books.Name);

                // 透過TSQL修改DB資料
                Console.WriteLine(books.Name);

                Category book2 = session.Load<Category>(14L);
                Assert.AreEqual("Books", book2.Name);
                //session.Transaction.Commit();
            }

            using (ISession session = _sessionFactory.OpenSession())
            {
                Category books = session.Get<Category>(14L);
                Assert.AreEqual("Book", books.Name);
                Console.WriteLine(books.Name);
            }
        }
    }
}
