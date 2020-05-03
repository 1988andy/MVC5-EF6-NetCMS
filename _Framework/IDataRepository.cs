using IBLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace _Framework
{
    public interface IDataRepository
    {
        T Update<T>(T entity) where T : ModelBase;
        T Insert<T>(T entity) where T : ModelBase;
        void Delete<T>(T entity) where T : ModelBase;
        T Find<T>(params object[] keyValues) where T : ModelBase;
        List<T> FindAll<T>(Expression<Func<T, bool>> conditions = null) where T : ModelBase;
        PagedList<T> FindAllByPage<T, S>(Expression<Func<T, bool>> conditions, Expression<Func<T, S>> orderBy, int pageSize, int pageIndex) where T : ModelBase;
    }
}
