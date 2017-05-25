using NHibernate.Dialect;

namespace NHibernate.FullText.Dialect
{
    public class MsSql2005FullTextDialect : MsSql2005Dialect
    {
        public MsSql2005FullTextDialect()
        {
            FullTextDialectHelper.Register(this);
        }
    }
}
