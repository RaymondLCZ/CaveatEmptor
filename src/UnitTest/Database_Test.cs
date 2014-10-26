using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using Zee.Sample.CaveatEmptor.Model;

namespace UnitTest
{
    [TestClass]
    public class Database_Test
    {
        private ISessionFactory _sessionFactory;
        private NHibernate.Cfg.Configuration _configuration;

        [TestInitialize]
        public void TestSetup()
        {
            _configuration = new NHibernate.Cfg.Configuration();
            _configuration.Configure();
            //_configuration.AddAssembly(typeof(Employee).Assembly);

            //_sessionFactory = _configuration.BuildSessionFactory();
        }

        [TestMethod]
        [TestCategory("DAO")]
        public void Can_Build_Database()
        {
            NHibernate.Tool.hbm2ddl.SchemaExport schemaExport = new SchemaExport(_configuration);
            schemaExport.SetOutputFile(@"D:\TestDB.txt");
            schemaExport.Execute(true, true, false);
        }

        [TestMethod()]
        [TestCategory("DAO")]
        public void Can_Drop_Database()
        {
            NHibernate.Tool.hbm2ddl.SchemaExport schemaExport = new SchemaExport(_configuration);
            schemaExport.Drop(true, true);
        }
    }
}
