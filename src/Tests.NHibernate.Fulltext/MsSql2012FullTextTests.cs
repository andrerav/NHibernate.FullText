using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate;
using NHibernate.FullText;
using NHibernate.Linq;
using Tests.NHibernate.Fulltext.Model;

namespace Tests.NHibernate.Fulltext
{
	[TestClass]
	public class MsSql2012FullTextTests
	{
		private static ISession session;

		[ClassInitialize]
		public static void ClassInitialize(TestContext context)
		{
			session = SessionManagement.CreateSessionFactory<MsSql2012FullTextDialect>().OpenSession();	
		}

		[ClassCleanup]
		public static void ClassCleanup()
		{
			if (session != null)
			{
				session.Dispose();
			}
		}


		[TestMethod]
		public void TestContainsQuery()
		{
			var content = session.Query<Content>().Where(x => x.Description.FullTextContains("great")).ToList();
			Assert.IsTrue(content.Count() == 1);
		}
		[TestMethod]
		public void TestFreetextQuery()
		{
			var content = session.Query<Content>().Where(x => x.Description.FreeText("great")).ToList();
			Assert.IsTrue(content.Count() == 1);
		}
	}
}
