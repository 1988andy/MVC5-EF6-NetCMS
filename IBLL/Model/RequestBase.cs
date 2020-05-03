using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL.Model
{
    public class RequestBase : ModelBase
    {
        /// <summary>
        /// 请求初始化每页记录数
        /// </summary>
        public RequestBase()
        {
            PageSize = 5000;
        }

        /// <summary>
        /// 前多少条记录
        /// </summary>
        public int Top
        {
            set
            {
                this.PageSize = value;
                this.PageIndex = 1;
            }
        }
        /// <summary>
        /// 每页记录数
        /// </summary>
        public int PageSize { get; set; }
        /// <summary>
        /// 当前页索引
        /// </summary>
        public int PageIndex { get; set; }
    }
}
