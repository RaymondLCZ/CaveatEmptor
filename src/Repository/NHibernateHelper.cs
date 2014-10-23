using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Cfg;

namespace Zee.Sample.CaveatEmptor.Repository
{
    public class NHibernateHelper
    {
        static ISessionFactory factory;
        static ISession session;
        static Configuration configuration;

        public static Configuration Configuration
        {
            get
            {
                return configuration;
            }

            set
            {
                configuration = value;
                factory = configuration.Configure().BuildSessionFactory();
                session = factory.OpenSession();
            }
        }

        static NHibernateHelper()
        {
            Configuration = new Configuration();
        }

        public static ISession OpenSession()
        {
            return session;
        }

        public static void CloseSession()
        {
            session.Close();
        }
    }
}
