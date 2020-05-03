using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Core
{
    public interface IConfigFileService
    {
        string GetConfig(string name);
        void SaveConfig(string name, string content);
        string GetFilePath(string name);
    }
}
