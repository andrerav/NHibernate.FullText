using NHibernate.Linq;
using NHibernate.Linq.Functions;

namespace NHibernate.FullText.Linq.Functions
{
    public class FullTextLinqtoHqlGeneratorsRegistry : DefaultLinqToHqlGeneratorsRegistry
    {
        public FullTextLinqtoHqlGeneratorsRegistry()
        {
            // ReSharper disable VirtualMemberCallInConstructor

            // Register "contains" function
            RegisterGenerator(ReflectionHelper.GetMethod(() => FullTextDialectExtensions.FullTextContains(null, null)), new MsSqlFullTextGenerator("contains", () => FullTextDialectExtensions.FullTextContains(null, null)));
            RegisterGenerator(ReflectionHelper.GetMethod(() => FullTextHelper.Contains((string[])null, null)), new MsSqlFullTextGenerator("contains", () => FullTextHelper.Contains((string[])null, null)));
            RegisterGenerator(ReflectionHelper.GetMethod(() => FullTextHelper.Contains((string)null, null)), new MsSqlFullTextGenerator("contains", () => FullTextHelper.Contains((string)null, null)));

            // Register "freetext" function		    
            RegisterGenerator(ReflectionHelper.GetMethod(() => FullTextDialectExtensions.FreeText(null, null)), new MsSqlFullTextGenerator("freetext", () => FullTextDialectExtensions.FreeText(null, null)));
            RegisterGenerator(ReflectionHelper.GetMethod(() => FullTextHelper.FreeText((string[])null, null)), new MsSqlFullTextGenerator("freetext", () => FullTextHelper.FreeText((string[])null, null)));
            RegisterGenerator(ReflectionHelper.GetMethod(() => FullTextHelper.FreeText((string)null, null)), new MsSqlFullTextGenerator("freetext", () => FullTextHelper.FreeText((string)null, null)));
        }
    }
}
