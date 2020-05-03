using _Core.Models;
using _Core.Utils;
using System.Web.Caching;

namespace _Core
{
    public class CachedConfigContext : ConfigContext
    {
        public static CachedConfigContext Current = new CachedConfigContext();

        /// <summary>
        /// 重写基类的取配置，加入缓存机制
        /// </summary>
        public override T Get<T>(string index = null)
        {
            var fileName = this.GetConfigFileName<T>(index);
            var key = "ConfigFile_" + fileName;
            var content = CachingHelper.Get(key);
            if (content != null) return (T)content;

            var value = base.Get<T>(index);
            CachingHelper.Set(key, value, new CacheDependency(ConfigService.GetFilePath(fileName)));
            return value;
        }
        /// <summary>
        /// 数据库连接配置
        /// </summary>
        public DaoConfig DaoConfig
        {
            get
            {
                return this.Get<DaoConfig>();
            }
        }
    }
}
