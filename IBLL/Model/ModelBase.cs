using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL.Model
{
    public class ModelBase
    {
        public ModelBase()
        {
            CreateTime = DateTime.Now;
        }

        public virtual int ID { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
    }
}
