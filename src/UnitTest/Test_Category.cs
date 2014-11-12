using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zee.Sample.CaveatEmptor.Model;
using NHibernate;

namespace UnitTest
{
    [TestClass]
    public class Test_Category
    {
        private ISessionFactory _sessionFactory;
        private NHibernate.Cfg.Configuration _configuration;

        public Test_Category()
        {
            _configuration = new NHibernate.Cfg.Configuration();
            _configuration.Configure();
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        [TestCategory("Category")]
        [TestMethod()]
        public void Can_Add_Category()
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    Category computer = new Category("Computer");
                    session.Save(computer);
                    session.Transaction.Commit();
                }
            }
        }

        [TestCategory("Category")]
        [TestMethod]
        public void Can_Add_ChildCategory()
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    Category computer = session.Get<Category>(1L);

                    Category laptops = new Category("Laptops");
                    laptops.ParentCategory = computer;
                    computer.ChildCategories.Add(laptops);

                    session.SaveOrUpdate(computer);
                    session.Transaction.Commit();
                }
            }
        }

    }
}