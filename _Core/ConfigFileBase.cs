using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Core
{
    public abstract class ConfigFileBase
    {
        public int Id { get; set; }

        public virtual bool ClusteredByIndex
        {
            get
            {
                return false;
            }
        }

        internal virtual void Save()
        {
        }

        internal virtual void UpdateNodeList<T>(List<T> nodeList)
        {
            ////重写id(index)
            //foreach (var node in nodeList)
            //{
            //    if (node.Id > 0)
            //        continue;

            //    node.Id = nodeList.Max(n => n.Id) + 1;
            //}
        }
    }
}
