using _Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Core
{
    public class ConfigContext
    {
        public IConfigFileService ConfigService { get; set; }

        /// <summary>
        /// 默认以文件形式存取配置
        /// </summary>
        public ConfigContext()
            : this(new ConfigFileService())
        {
        }

        public ConfigContext(IConfigFileService pageContentConfigService)
        {
            this.ConfigService = pageContentConfigService;
        }

        public virtual T Get<T>(string index = null) where T : ConfigFileBase, new()
        {
            var result = new T();
            result = this.GetConfigFile<T>(index);

            return result;
        }

        public void Save<T>(T configFile, string index = null) where T : ConfigFileBase
        {
            configFile.Save();

            var fileName = this.GetConfigFileName<T>(index);
            this.ConfigService.SaveConfig(fileName, SerializationHelper.XmlSerialize(configFile));
        }

        private T GetConfigFile<T>(string index = null) where T : ConfigFileBase, new()
        {
            var result = new T();

            var fileName = this.GetConfigFileName<T>(index);
            var content = this.ConfigService.GetConfig(fileName);
            if (content == null)
            {
                this.ConfigService.SaveConfig(fileName, string.Empty);
            }
            else if (!string.IsNullOrEmpty(content))
            {
                try
                {
                    result = (T)SerializationHelper.XmlDeserialize(typeof(T), content);
                }
                catch
                {
                    result = new T();
                }
            }

            return result;
        }

        public virtual string GetConfigFileName<T>(string index = null)
        {
            var fileName = typeof(T).Name;
            if (!string.IsNullOrEmpty(index))
                fileName = string.Format("{0}_{1}", fileName, index);
            return fileName;
        }
    }
}
