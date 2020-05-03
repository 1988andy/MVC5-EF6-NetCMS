using _Core;
using _Framework;
using IBLL.Model.Cms;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CmsDbContext : DbContextBase
    {
        public CmsDbContext()
            : base(CachedConfigContext.Current.DaoConfig.Application)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<CmsDbContext>());
            Database.SetInitializer<CmsDbContext>(null);

            modelBuilder.Entity<Article>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Articles)
                .Map(m =>
                {
                    m.ToTable("Cms_ArticleTag");
                    m.MapLeftKey("ArticleId");
                    m.MapRightKey("TagId");
                });

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Article> Articles { get; set; }
        public DbSet<Channel> Channels { get; set; }
        public DbSet<Tag> Tags { get; set; }
    }
}
