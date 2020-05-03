using IBLL;
using IBLL.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace _Framework
{
    /// <summary>
    ///  DAL基类，实现Repository通用泛型数据访问模式
    /// </summary>
    public class DbContextBase : DbContext, IDataRepository, IDisposable
    {
        /// <summary>
        /// 数据库连接上下文基类构造函数
        /// </summary>
        /// <param name="connectionString"></param>
        public DbContextBase(string connectionString)
        {
            //var objectContext = (this as IObjectContextAdapter).ObjectContext;
            //objectContext.CommandTimeout = 500;

            this.Database.Connection.ConnectionString = connectionString;
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
        }

        public DbContextBase(string connectionString, IAuditable auditLogger)
            : this(connectionString)
        {
            this.AuditLogger = auditLogger;
        }

        public IAuditable AuditLogger { get; set; }

        public T Update<T>(T entity) where T : ModelBase
        {
            var set = this.Set<T>();
            set.Attach(entity);
            this.Entry<T>(entity).State = EntityState.Modified;
            this.SaveChanges();
            return entity;
        }

        public T Insert<T>(T entity) where T : ModelBase
        {
            this.Set<T>().Add(entity);
            this.SaveChanges();
            return entity;
        }

        public List<T> InsertAll<T>(List<T> entity) where T : ModelBase
        {
            foreach (var t in entity)
            {
                this.Insert<T>(t);
            }
            return entity;
        }

        public void Delete<T>(T entity) where T : ModelBase
        {
            this.Entry<T>(entity).State = EntityState.Deleted;
            this.SaveChanges();
        }

        public void DeleteAll<T>(List<T> entity) where T : ModelBase
        {
            foreach (var t in entity)
            {
                this.Delete<T>(t);
            }
        }

        public T Find<T>(params object[] keyValues) where T : ModelBase
        {
            return this.Set<T>().Find(keyValues);
        }

        public List<T> FindAll<T>(Expression<Func<T, bool>> conditions = null) where T : ModelBase
        {
            return conditions == null ? this.Set<T>().ToList() : this.Set<T>().Where(conditions).ToList();
        }

        public PagedList<T> FindAllByPage<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int pageSize, int pageIndex) where T : ModelBase
        {
            var queryList = conditions == null ? this.Set<T>() : this.Set<T>().Where(conditions) as IQueryable<T>;

            return queryList.OrderByDescending(orderBy).ToPagedList(pageIndex, pageSize);
        }

        public override int SaveChanges()
        {
            //this.WriteAuditLog();

            //调试具体哪个字段报错
            int result = 0;
            //try
            //{
                result = base.SaveChanges();
            //}
            //catch (DbEntityValidationException e)
            //{
            //    string err = string.Empty;
            //    foreach (var eve in e.EntityValidationErrors)
            //    {
            //        err = eve.ValidationErrors.ToString();
            //    }
            //}

            return result;
        }
    }
}
