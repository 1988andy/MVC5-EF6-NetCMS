using IBLL.Model;
using IBLL.Model.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL
{
    public interface ITestService
    {
        Article GetArticle(int id);
        Channel GetChannel(int id);
        IEnumerable<Channel> GetChannelList(ChannelRequest request = null);
        IEnumerable<Article> GetArticleList(ArticleRequest request = null);
        IEnumerable<Tag> GetTagList(TagRequest request = null);
        void SaveArticle(Article article);
        void SaveArticle(Article article, out Article outArticle);
        void DeleteArticle(int id);
        void DeleteArticle(List<int> ids);
    }
}
