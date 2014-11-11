using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Zee.Sample.CaveatEmptor.Model;
using Zee.Sample.CaveatEmptor.Repository;
using NHibernate;

namespace UnitTest
{

    /// <summary>
    /// Test_User 的摘要描述
    /// </summary>
    [TestClass]
    public class Test_User
    {
        private ISessionFactory _sessionFactory;
        private NHibernate.Cfg.Configuration _configuration;

        public Test_User()
        {
            _configuration = new NHibernate.Cfg.Configuration();
            _configuration.Configure();
        }


        #region 其他測試屬性
        //
        // 您可以使用下列其他屬性撰寫您的測試:
        //
        // 執行該類別中第一項測試前，使用 ClassInitialize 執行程式碼
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // 在類別中的所有測試執行後，使用 ClassCleanup 執行程式碼
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // 在執行每一項測試之前，先使用 TestInitialize 執行程式碼 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // 在執行每一項測試之後，使用 TestCleanup 執行程式碼
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestCategory("User")]
        [TestMethod]
        public void Can_Add_User()
        {
            UserRepository dao = new UserRepository();

            User user = new User("Raymond", "Lam", "raymond", "1234", "raymond@ceci.com.tw");
            user.HomeAddress = new Address("one street", "214", "Taipei");
            user.BillingAddress = new Address("one street", "214", "Taipei");
            user.IsAdmin = false;

            dao.SaveOrUpdate(user);
        }

        [TestCategory("User")]
        [TestMethod]
        public void Can_Find_User()
        {
            UserRepository dao = new UserRepository();

            IList<User> users = dao.FindAdd();
            User raymond = dao.FindBy(1L);

            Assert.AreEqual("Raymond", raymond.Firstname);
        }

        [TestCategory("User")]
        [TestMethod]
        public void Can_Add_Item()
        {
            UserRepository dao = new UserRepository();
            User raymond = dao.FindBy(1L);

            Item theItem = new Item("TestItem", "A item for test", raymond, new MonetaryAmount(100, "USD"), new MonetaryAmount(150, "USD"), DateTime.Today, DateTime.Today.AddMonths(1));

            raymond.AddItem(theItem);

            dao.SaveOrUpdate(raymond);
        }

    }
}
