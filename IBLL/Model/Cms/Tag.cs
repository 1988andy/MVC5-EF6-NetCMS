using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IBLL.Model.Cms
{
    [Serializable]
    [Table("Cms_Tag")]
    public class Tag : ModelBase
    {
        public Tag()
        {

        }

        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        public int Hits { get; set; }

        public virtual List<Article> Articles { get; set; }
    }
}
