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
                    // �p�G ParentCategory �� many-to-one ��cascade="save-update", save�l����|�۰ʦV�Wtransitive�F�Ϥ��p�]��none�h�|�X�{exception.
                    //session.Save(travelMagazine);

                    // save ������n�۰ʦV�Utransitive�A�ݭn�b set ���] cascade="save-update"    
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