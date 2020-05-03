using IBLL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IBLL
{
    /// <summary>
    /// 用于写数据（修改，添加等）历史日志
    /// </summary>
    public interface IAuditable
    {
        void WriteLog(int modelId, string userName, string moduleName, string tableName, string eventType, ModelBase newValues);
    }
}
