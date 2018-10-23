using System;
using System.Linq;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using NHibernate;
using NHibernate.Dialect;
using NHibernate.FullText;
using NHibernate.FullText.Dialect;
using NHibernate.Linq;
using Tests.NHibernate.Fulltext.Model;

namespace Tests.NHibernate.Fulltext
{
	public class MsSql2014FullTextTests
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
		public void TestContainsQuery1()
		{
			var content = session.Query<Content>().Where(x => x.Description.FullTextContains("great")).ToList();
			Assert.IsTrue(content.Count() == 1);
		}

		[TestMethod]
		public void TestFreetextQuery1()
		{
			var content = session.Query<Content>().Where(x => x.Description.FreeText("great")).ToList();
			Assert.IsTrue(content.Count() == 1);
		}

		[TestMethod]
		public void TestContainsQuery2()
		{
			var content = session.Query<Content>().Where(x => x.Description.FullTextContains("NHibernate")).ToList();
			Assert.IsTrue(content.Count() == 2);
		}

		[TestMethod]
		public void TestFreetextQuery2()
		{
			var content = session.Query<Content>().Where(x => x.Description.FreeText("NHibernate")).ToList();
			Assert.IsTrue(content.Count() == 2);
		}

		[TestMethod]
		public void TestStopword1()
		{
			var content = session.Query<Content>().Where(x => x.Description.FullTextContains("is")).ToList();
			Assert.IsTrue(!content.Any());
		}

		[TestMethod]
		public void TestStopword2()
		{
			var content = session.Query<Content>().Where(x => x.Description.FreeText("is")).ToList();
			Assert.IsTrue(!content.Any());
		}

	}
}
