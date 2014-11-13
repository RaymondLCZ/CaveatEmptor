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
            Category publications = new Category("Publications");
            Category books = new Category("Books");
            Category magazine = new Category("Magazine");

            publications.AddChildCategory(books);
            publications.AddChildCategory(magazine);

            Category travelMagazine = new Category("Travel Magazine");
            magazine.AddChildCategory(travelMagazine);

            using (ISession session = _sessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    // 如果 ParentCategory 中 many-to-one 的cascade="save-update", save子物件會自動向上transitive；反之如設為none則會出現exception.
                    //session.Save(travelMagazine);

                    // save 父物件要自動向下transitive，需要在 set 中設 cascade="save-update"    
                    session.Save(publications);
                    session.Transaction.Commit();
                }
            }
        }

        [TestCategory("Category")]
        [TestMethod]
        public void Can_Add_ChildCategory()
        {
            Category electronics = new Category("Electronics");
            Category computer = new Category("Computer");
            //computer.ParentCategory = electronics;
            electronics.AddChildCategory(computer);

            using (ISession session = _sessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {                    
                    session.Save(electronics);
                    session.Transaction.Commit();
                }
            }


            Category laptops = new Category("Laptops");
            laptops.ParentCategory = computer;
            computer.ChildCategories.Add(laptops);

            using (ISession session = _sessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    session.Save(laptops);
                    //session.SaveOrUpdate(computer);
                    session.Transaction.Commit();
                }
            }
        }

    }
}