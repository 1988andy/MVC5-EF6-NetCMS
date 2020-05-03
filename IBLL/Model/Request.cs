using IBLL.Model.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL.Model
{
    public class ArticleRequest : RequestBase
    {
        public ArticleRequest()
        {
            this.Article = new Article();
        }
        public override int ID { get; set; }
        public string Title { get; set; }
        public int ChannelId { get; set; }
        public bool? IsActive { get; set; }
        public int UserID { get; set; }
        public bool IsProposed { get; set; }
        public Article Article { get; set; }
        public List<int> ChannelIds { get; set; }
    }

    public class ChannelRequest : RequestBase
    {
        public string Name { get; set; }
        public string Flag { get; set; }
        public int ParentId { get; set; }
        public bool? IsActive { get; set; }
    }

    public class TagRequest : RequestBase
    {
        public override int ID { get; set; }
        public Orderby Orderby { get; set; }
    }

    public class TagArticleRequest : RequestBase
    {
        public int TagId { get; set; }
    }

    public enum Orderby
    {
        ID = 0,
        Hits = 1
    }
}
