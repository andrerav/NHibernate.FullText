using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;

namespace Tests.NHibernate.Fulltext.Model
{
	public class Content
	{
		public virtual string PageName { get; set; }
		public virtual string Url { get; set; }
		public virtual string Description { get; set; }
		public virtual string Keywords { get; set; }
	}

	class ContentMap : ClassMap<Content>
    {
        //Constructor
		public ContentMap()
        {
			Id(x => x.PageName);
            Map(x => x.Url);
            Map(x => x.Description);
            Map(x => x.Keywords);
            Table("Content");
        }
    }
}
