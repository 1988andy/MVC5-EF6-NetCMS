using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace _Core.Models
{
    public class DaoConfig : ConfigFileBase
    {
        public DaoConfig()
        {
        }

        public String Application { get; set; }
    }
}
