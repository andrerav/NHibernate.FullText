using System.Collections.ObjectModel;
using System.Linq.Expressions;
using System.Reflection;
using NHibernate.Hql.Ast;
using NHibernate.Linq;
using NHibernate.Linq.Functions;
using NHibernate.Linq.Visitors;
using NHibernate.Util;

namespace NHibernate.FullText.Linq.Functions
{
    /// <summary>
    /// Based on code from http://stackoverflow.com/questions/21059722/nhibernate-linqtohqlgenerator-for-sql-server-2008-full-text-index-containing-k
    /// </summary>
    public class MsSqlFullTextGenerator : BaseHqlGeneratorForMethod
    {
        protected string FunctionName;

        public MsSqlFullTextGenerator(string functionName, Expression<System.Action> expression)
        {
            FunctionName = functionName;
            SupportedMethods = new[] { ReflectHelper.GetMethod(expression) };
        }

        public override HqlTreeNode BuildHql(MethodInfo method, Expression targetObject, ReadOnlyCollection<Expression> arguments, HqlTreeBuilder treeBuilder, IHqlExpressionVisitor visitor)
        {
            return treeBuilder.BooleanMethodCall(FunctionName, new[]
            {
                treeBuilder.MethodCall(string.Empty, visitor.Visit(arguments[0]).AsExpression()),

                visitor.Visit(arguments[1]).AsExpression()
            });
        }
    }
}
