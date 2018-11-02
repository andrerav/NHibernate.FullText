using NHibernate.Linq;
using NHibernate.Linq.Functions;
using NHibernate.Util;

namespace NHibernate.FullText.Linq.Functions
{
    public class FullTextLinqtoHqlGeneratorsRegistry : DefaultLinqToHqlGeneratorsRegistry
    {
        public FullTextLinqtoHqlGeneratorsRegistry()
        {
            // ReSharper disable VirtualMemberCallInConstructor

            // Register "contains" function
            RegisterGenerator(ReflectHelper.GetMethod(() => FullTextDialectExtensions.FullTextContains(null, null)), new MsSqlFullTextGenerator("contains", () => FullTextDialectExtensions.FullTextContains(null, null)));
            RegisterGenerator(ReflectHelper.GetMethod(() => FullTextHelper.Contains((string[])null, null)), new MsSqlFullTextGenerator("contains", () => FullTextHelper.Contains((string[])null, null)));
            RegisterGenerator(ReflectHelper.GetMethod(() => FullTextHelper.Contains((string)null, null)), new MsSqlFullTextGenerator("contains", () => FullTextHelper.Contains((string)null, null)));

            // Register "freetext" function
            RegisterGenerator(ReflectHelper.GetMethod(() => FullTextDialectExtensions.FreeText(null, null)), new MsSqlFullTextGenerator("freetext", () => FullTextDialectExtensions.FreeText(null, null)));
            RegisterGenerator(ReflectHelper.GetMethod(() => FullTextHelper.FreeText((string[])null, null)), new MsSqlFullTextGenerator("freetext", () => FullTextHelper.FreeText((string[])null, null)));
            RegisterGenerator(ReflectHelper.GetMethod(() => FullTextHelper.FreeText((string)null, null)), new MsSqlFullTextGenerator("freetext", () => FullTextHelper.FreeText((string)null, null)));
        }
    }
}
