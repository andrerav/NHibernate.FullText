using NHibernate.Dialect;

namespace NHibernate.FullText.Dialect
{
    public class MsSql2008FullTextDialect : MsSql2008Dialect
    {
        public MsSql2008FullTextDialect()
        {
            FullTextDialectHelper.Register(this);
        }
    }
}
