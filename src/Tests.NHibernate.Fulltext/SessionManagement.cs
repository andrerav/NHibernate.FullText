using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using NHibernate;
using NHibernate.Dialect;
using NHibernate.FullText;
using Tests.NHibernate.Fulltext.Model;

namespace Tests.NHibernate.Fulltext
{
	public static class SessionManagement
	{
		public static ISessionFactory CreateSessionFactory<T>() where T:Dialect
		{
			return Fluently.Configure()
			  .Database(
				FluentNHibernate.Cfg.Db.MsSqlConfiguration.MsSql2012
					.ConnectionString("server=localhost;Integrated Security=SSPI;database=NHFullTextTest")
					.Dialect<T>()
			  )
			  .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Content>())
			  .BuildSessionFactory();
		}
	}
}
