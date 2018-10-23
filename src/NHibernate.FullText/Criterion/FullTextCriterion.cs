using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Criterion;
using NHibernate.Engine;
using NHibernate.SqlCommand;

namespace NHibernate.FullText.Criterion
{
    [Serializable]
    internal class FullTextCriterion : AbstractCriterion
    {
        private readonly string _functionName;
        private readonly IProjection[] _projections;
        private readonly string _pattern;
        private readonly int? _language;
        private readonly TypedValue _typedValue;

        public FullTextCriterion(string functionName, IProjection[] projections, string pattern,int? language)
        {
            _functionName = functionName;
            _projections = projections;
            _pattern = pattern;
            _language = language;

            _typedValue = new TypedValue(NHibernateUtil.String, pattern);

        }
        public override string ToString()
        {
            var columns = string.Join(", ", _projections.Select(p => p.ToString()));

            if(_language.HasValue)
                string.Format("{0}(({1}), '{2}', language {3})", _functionName, columns, _pattern, _language);

            return string.Format("{0}(({1}), '{2}')", _functionName, columns, _pattern);
        }

        public override SqlString ToSqlString(ICriteria criteria, ICriteriaQuery criteriaQuery)
        {
            var namesUsingProjections = _projections.Select(p => SqlStringHelper.RemoveAsAliasesFromSql(p.ToSqlString(criteria, criteriaQuery.GetIndexForAlias(), criteriaQuery))).ToArray();

            var parameter = criteriaQuery.NewQueryParameter(_typedValue).Single();

            var sqlStringBuilder = new SqlStringBuilder();

            sqlStringBuilder.Add(_functionName);
            sqlStringBuilder.Add("(");
            sqlStringBuilder.Add("(");
            for (var i = 0; i < namesUsingProjections.Length; i++)
            {
                sqlStringBuilder.Add(namesUsingProjections[0]);
                if (i != namesUsingProjections.Length - 1)
                    sqlStringBuilder.Add(",");
            }

            sqlStringBuilder.Add(")");

            sqlStringBuilder.Add(",");
            sqlStringBuilder.Add(parameter);

            if (_language.HasValue)
                sqlStringBuilder.Add(string.Format(",language {0}", _language));

            sqlStringBuilder.Add(")");

            return sqlStringBuilder.ToSqlString();
        }

        public override TypedValue[] GetTypedValues(ICriteria criteria, ICriteriaQuery criteriaQuery)
        {
            return new[] { _typedValue };
        }

        public override IProjection[] GetProjections()
        {
            return _projections;
        }
    }
}
