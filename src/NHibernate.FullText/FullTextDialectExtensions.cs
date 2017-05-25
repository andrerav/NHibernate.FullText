using System;

namespace NHibernate.FullText
{
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
            throw new NotImplementedException();
        }

        /// <summary>
        /// This predicate searches for values that match the meaning and not just the exact wording of the words in the search condition.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool FreeText(this string source, string pattern)
        {
            throw new NotImplementedException();
        }
    }
}
