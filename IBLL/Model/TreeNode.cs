using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL.Model
{
    /// <summary>
    /// 树节点模型
    /// </summary>
    public class TreeNode
    {
        public int ID { get; set; }

        public int level { get; set; }

        public int parentID { get; set; }

        public string Name { get; set; }

        public string Class { get; set; }
    }
}
