using System.Reflection;
using NHibernate.Dialect.Function;
using NHibernate.FullText.Linq.Functions;

namespace NHibernate.FullText.Dialect
{
    public class FullTextDialectHelper
    {
        private static readonly MethodInfo RegisterFunctionMethod = typeof(NHibernate.Dialect.Dialect).GetMethod("RegisterFunction", BindingFlags.Instance | BindingFlags.NonPublic);

        public static void Register(NHibernate.Dialect.Dialect dialect)
        {
            // Tell NHibernate that contains() and freetext() are available as SQL functions
            RegisterFunction(dialect,"contains", new StandardSQLFunction("contains", null));
            RegisterFunction(dialect,"freetext", new StandardSQLFunction("freetext", null));
            
            // This trick prevents the need to configure the generator specifically
            dialect.DefaultProperties["linqtohql.generatorsregistry"] = typeof(FullTextLinqtoHqlGeneratorsRegistry).AssemblyQualifiedName;
        }

        internal static void RegisterFunction(NHibernate.Dialect.Dialect dialect, string name, ISQLFunction function)
        {
            RegisterFunctionMethod.Invoke(dialect, new object[] { name, function });
        }
    }
}
