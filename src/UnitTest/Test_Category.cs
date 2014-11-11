using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zee.Sample.CaveatEmptor.Model;
using Zee.Sample.CaveatEmptor.Repository;
using NHibernate;

namespace UnitTest
{
    [TestClass]
    public class Test_Category
    {
        private ISessionFactory _sessionFactory;
        private NHibernate.Cfg.Configuration _configuration;

        // 執行該類別中第一項測試前，使用 ClassInitialize 執行程式碼
        [ClassInitialize()]
        public static void MyClassInitialize(TestContext testContext)
        {
        }

        // 在執行每一項測試之前，先使用 TestInitialize 執行程式碼 
        [TestInitialize()]
        public void MyTestInitialize()
        {
            _configuration = new NHibernate.Cfg.Configuration();
            _configuration.Configure();
            _sessionFactory = _configuration.BuildSessionFactory();
        }

        [TestCategory("Category")]
        [TestMethod]
        public void Can_Add_Category()
        {
            using (ISession session = _sessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    Category electronics = new Category("Electronics");
                    session.Save(electronics);
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
                    Category electronics = session.Get<Category>(3L);
                    Category cellPhones = new Category("Cell Phones");

                    cellPhones.ParentCategory = electronics;
                    electronics.ChildCategories.Add(cellPhones);

                    session.Transaction.Commit();
                }
            }
        }

        [TestCategory("Category")]
        [TestMethod]
        public void Can_Add_ChildCategory_Outside_Session()
        {
            Category electronics;
            using (ISession session = _sessionFactory.OpenSession())
            {
                electronics = session.Get<Category>(3L);
            }

            Category computer = new Category("Computer");
            computer.ParentCategory = electronics;
            electronics.ChildCategories.Add(computer);

            using (ISession session = _sessionFactory.OpenSession())
            {
                using (session.BeginTransaction())
                {
                    session.Save(computer);
                    session.Transaction.Commit();
                }
            }
        }

    }
}
