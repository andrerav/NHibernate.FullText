using NHibernate.Criterion;

namespace NHibernate.FullText.Criterion
{
    public static class FullTextExpression
    {
        public static AbstractCriterion Contains(IProjection[] projections, string pattern, int language)
        {
            return new FullTextCriterion("contains", projections, pattern, language);
        }

        public static AbstractCriterion Contains(IProjection[] projections, string pattern)
        {
            return new FullTextCriterion("contains", projections, pattern, null);
        }

        public static AbstractCriterion Contains(IProjection projection, string pattern, int language)
        {
            return new FullTextCriterion("contains", new[] { projection }, pattern, language);
        }

        public static AbstractCriterion Contains(IProjection projection, string pattern)
        {
            return new FullTextCriterion("contains", new[] { projection }, pattern, null);
        }

        public static AbstractCriterion FreeText(IProjection[] projections, string pattern, int language)
        {
            return new FullTextCriterion("freetext", projections, pattern, language);
        }

        public static AbstractCriterion FreeText(IProjection[] projections, string pattern)
        {
            return new FullTextCriterion("freetext", projections, pattern, null);
        }

        public static AbstractCriterion FreeText(IProjection projection, string pattern, int language)
        {
            return new FullTextCriterion("freetext" , new[] { projection }, pattern, language);
        }

        public static AbstractCriterion FreeText(IProjection projection, string pattern)
        {
            return new FullTextCriterion("freetext", new[] { projection }, pattern, null);
        }
    }
}
