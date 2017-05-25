using NHibernate.Dialect;

namespace NHibernate.FullText.Dialect
{
    public class MsSql2012FullTextDialect : MsSql2012Dialect
    {
        public MsSql2012FullTextDialect()
        {
            FullTextDialectHelper.Register(this);
        }
    }
}
