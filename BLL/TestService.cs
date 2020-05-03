using DAL;
using IBLL;
using IBLL.Model;
using IBLL.Model.Cms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _Framework;

namespace BLL
{
    public class TestService : ITestService
    {
        public Article GetArticle(int id)
        {
            using (var dbContext = new CmsDbContext())
            {
                return dbContext.Articles.Include("Tags").FirstOrDefault(a => a.ID == id);
            }
        }

        public Channel GetChannel(int id)
        {
            using (var dbContext = new CmsDbContext())
            {
                return dbContext.Find<Channel>(id);
            }
        }

        public IEnumerable<Channel> GetChannelList(ChannelRequest request = null)
        {
            request = request ?? new ChannelRequest();
            using (var dbContext = new CmsDbContext())
            {
                IQueryable<Channel> channels = dbContext.Channels;
                //频道名称
                if (!string.IsNullOrWhiteSpace(request.Name))
                    channels = channels.Where(u => u.Name.Contains(request.Name));
                //频道ID
                if (request.ID > 0)
                    channels = channels.Where(t => t.ID == request.ID);
                //父类ID
                if (request.ParentId >= 0)
                    channels = channels.Where(t => t.ParentId == request.ParentId);
                //状态
                if (request.IsActive != null)
                    channels = channels.Where(u => u.IsActive == request.IsActive);

                return channels.OrderBy(u => u.Flag).ToList();
            }
        }

        public IEnumerable<Article> GetArticleList(ArticleRequest request = null)
        {
            request = request ?? new ArticleRequest();
            using (var dbContext = new CmsDbContext())
            {
                IQueryable<Article> articles = dbContext.Articles.Include("Channel");
                if (request.ID > 0)
                    articles = articles.Where(u => u.ID == request.ID);

                if (!string.IsNullOrWhiteSpace(request.Title))
                {
                    if (request.Title.IndexOf('|') > 0)
                    {
                        string[] words = request.Title.Split('|');
                        List<Article> articleFlags = new List<Article>();
                        foreach (string w in words)
                        {
                            articleFlags.AddRange(articles.Where(u => u.Title.Contains(w)));
                        }
                        articles = articleFlags.Distinct().AsQueryable();
                    }
                    else
                    {
                        articles = articles.Where(u => u.Title.Contains(request.Title));
                    }
                }

                if (request.ChannelId > 0)
                    articles = articles.Where(u => u.ChannelId == request.ChannelId);

                if (request.IsActive != null)
                    articles = articles.Where(u => u.IsActive == request.IsActive);

                if (request.IsProposed)
                    articles = articles.Where(u => u.IsProposed == true);

                if (request.ChannelIds != null && request.ChannelIds.Count > 0)
                    articles = articles.Where(u => request.ChannelIds.Contains(u.ChannelId));

                return articles.OrderByDescending(u => u.ID).ToPagedList(request.PageIndex, request.PageSize);
            }
        }

        public IEnumerable<Tag> GetTagList(TagRequest request = null)
        {
            request = request ?? new TagRequest();
            using (var dbContext = new CmsDbContext())
            {
                IQueryable<Tag> tags = dbContext.Tags;

                if (request.ID > 0)
                    tags = tags.Where(u => u.ID == request.ID);

                if (request.Orderby == Orderby.Hits)
                    tags = tags.OrderByDescending(u => u.Hits);
                else
                    tags = tags.OrderByDescending(u => u.ID);

                return tags.ToPagedList(request.PageIndex, request.PageSize);
            }
        }

        public void SaveArticle(Article article)
        {
            using (var dbContext = new CmsDbContext())
            {
                var tags = new List<Tag>();

                if (article.Tags != null)
                {
                    foreach (var tag in article.Tags)
                    {
                        var existTag = dbContext.Tags.FirstOrDefault(t => t.Name == tag.Name);
                        if (existTag != null)
                            existTag.Hits++;
                        tags.Add(existTag ?? tag);
                    }
                }

                if (article.ID > 0)
                {
                    article.TagString = string.Empty;
                    dbContext.Update<Article>(article);
                    dbContext.Entry(article).Collection(m => m.Tags).Load();
                    article.Tags = tags;
                    dbContext.SaveChanges();
                }
                else
                {
                    article.Tags = tags;
                    dbContext.Insert<Article>(article);
                }
            }
        }

        public void SaveArticle(Article article, out Article outArticle)
        {
            using (var dbContext = new CmsDbContext())
            {
                var tags = new List<Tag>();

                if (article.Tags != null)
                {
                    foreach (var tag in article.Tags)
                    {
                        var existTag = dbContext.Tags.FirstOrDefault(t => t.Name == tag.Name);
                        if (existTag != null)
                            existTag.Hits++;
                        tags.Add(existTag ?? tag);
                    }
                }

                outArticle = null;

                if (article.ID > 0)
                {
                    article.TagString = string.Empty;
                    dbContext.Update<Article>(article);
                    dbContext.Entry(article).Collection(m => m.Tags).Load();
                    article.Tags = tags;
                    dbContext.SaveChanges();
                }
                else
                {
                    article.Tags = tags;
                    var model = dbContext.Insert<Article>(article);
                    outArticle = model;
                }
            }
        }

        public void DeleteArticle(int id)
        {
            using (var dbContext = new CmsDbContext())
            {
                dbContext.Articles.Include("Tags").Where(u => id == u.ID).ToList().ForEach(a => { a.Tags.Clear(); dbContext.Articles.Remove(a); });
                dbContext.SaveChanges();
            }
        }

        public void DeleteArticle(List<int> ids)
        {
            using (var dbContext = new CmsDbContext())
            {
                dbContext.Articles.Include("Tags").Where(u => ids.Contains(u.ID)).ToList().ForEach(a => { a.Tags.Clear(); dbContext.Articles.Remove(a); });
                dbContext.SaveChanges();
            }
        }
    }
}
