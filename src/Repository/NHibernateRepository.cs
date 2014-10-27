using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Zee.Sample.CaveatEmptor.Model;
using NHibernate;

namespace Zee.Sample.CaveatEmptor.Repository
{
    public class NHibernateRepository<T> where T : class
    {
        public void SaveOrUpdate(T t)
        {
            ITransaction trans = null;
            ISession session = null;
            try
            {
                session = NHibernateHelper.OpenSession();
                trans = session.BeginTransaction();
                //session.SaveOrUpdate(t);
                session.Save(t);
                session.Flush();
                trans.Commit();

            }
            catch (Exception ex)
            {
                trans.Rollback();
                session.Flush();
                throw ex;
            }
        }

        public  T FindBy(Object id) 
        {
            ISession session = NHibernateHelper.OpenSession();
            return session.Get<T>(id);
        }        

        public IList<T> FindAdd()
        {
            ISession session = NHibernateHelper.OpenSession();
            ICriteria criteria = session.CreateCriteria<T>();
            IList<T> results = criteria.List<T>();

            return results;
        }
    }
}
