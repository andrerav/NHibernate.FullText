// Copyright © 2015 Andreas Ravnestad
//
// NHibernate.FullText is free software; you can redistribute it and/or modify
// it under the terms of the GNU Lesser General Public License as published by
// the Free Software Foundation; either version 2 of the License, or
// (at your option) any later version.
//
// NHibernate.FullText is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the
// GNU Lesser General Public License for more details.
// You should have received a copy of the GNU Lesser General Public License
// along with NHibernate.FullText; if not, write to the Free Software
// Foundation, Inc., 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA 

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using NHibernate.Dialect.Function;
using NHibernate.Hql.Ast;
using NHibernate.Linq;
using NHibernate.Linq.Functions;
using NHibernate.Linq.Visitors;

namespace NHibernate.FullText
{
	public class MsSql2008FullTextDialect : Dialect.MsSql2008Dialect
	{
		public MsSql2008FullTextDialect()
		{
			// Tell NHibernate that contains() and freetext() are available as SQL functions
			RegisterFunction("contains", new StandardSQLFunction("contains", null));
			RegisterFunction("freetext", new StandardSQLFunction("freetext", null));

			// This trick prevents the need to configure the generator specifically
			DefaultProperties["linqtohql.generatorsregistry"] 
					= typeof(FullTextLinqtoHqlGeneratorsRegistry).AssemblyQualifiedName;
		}
	}

	public class MsSql2012FullTextDialect : MsSql2008FullTextDialect
	{
	}

	public class MsSql2014FullTextDialect : MsSql2012FullTextDialect
	{
	}

	/// <summary>
	/// Placeholders for LINQ extension methods
	/// </summary>
	public static class FullTextDialectExtensions
	{
		/// <summary>
		/// Searches for precise or fuzzy (less precise) matches to single words and phrases, words within a certain distance of one another, or weighted matches.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="pattern"></param>
		/// <returns></returns>
		public static bool FullTextContains(this string source, string pattern)
		{
			return false;
		}

		/// <summary>
		/// This predicate searches for values that match the meaning and not just the exact wording of the words in the search condition.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="pattern"></param>
		/// <returns></returns>
		public static bool FreeText(this string source, string pattern)
		{
			return false;
		}
	}

	public class FullTextLinqtoHqlGeneratorsRegistry : DefaultLinqToHqlGeneratorsRegistry
	{
		public FullTextLinqtoHqlGeneratorsRegistry()
			: base()
		{

			// Register "contains" function
			RegisterGenerator(ReflectionHelper.GetMethod(() => FullTextDialectExtensions.FullTextContains(null, null)),
						  new MsSql2008FullTextGenerator("contains", () => FullTextDialectExtensions.FullTextContains(null, null)));

			// Register "freetext" function
			RegisterGenerator(ReflectionHelper.GetMethod(() => FullTextDialectExtensions.FreeText(null, null)),
						  new MsSql2008FullTextGenerator("freetext", () => FullTextDialectExtensions.FreeText(null, null)));
		}
	}


	/// <summary>
	/// Based on code from http://stackoverflow.com/questions/21059722/nhibernate-linqtohqlgenerator-for-sql-server-2008-full-text-index-containing-k
	/// </summary>
	public class MsSql2008FullTextGenerator : BaseHqlGeneratorForMethod
	{
		// This should be either "contains" or "freetext"
		protected string FunctionName;

		// Constructor
		public MsSql2008FullTextGenerator(string functionName, Expression<System.Action> expression)
		{
			FunctionName = functionName;
			SupportedMethods = new[] { ReflectionHelper.GetMethod(expression) };
		}

		// Implement visitor for this node
		public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments,
		  HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
		{
			var args = new HqlExpression[2]
			{
				visitor.Visit(arguments[0]).AsExpression(), 
				visitor.Visit(arguments[1]).AsExpression()
			};

			return treeBuilder.BooleanMethodCall(FunctionName, args);
		}
	}
}
