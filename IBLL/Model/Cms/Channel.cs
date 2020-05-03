using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace IBLL.Model.Cms
{
    [Serializable]
    [Table("Cms_Channel")]
    public class Channel : ModelBase
    {
        public Channel()
        {

        }

        [StringLength(100, ErrorMessage = "频道名称不能超过100个字")]
        [Required(ErrorMessage = "频道名称不能为空")]
        public string Name { get; set; }

        [StringLength(20, ErrorMessage = "频道标识不能超过20个字符")]
        [Required(ErrorMessage = "频道标识不能为空")]
        public string Flag { get; set; }

        [StringLength(300, ErrorMessage = "图片路径不能超过300个字符")]
        public string CoverPicture { get; set; }

        [StringLength(300, ErrorMessage = "缩略图路径不能超过300个字符")]
        public string InnerPicture { get; set; }

        [StringLength(300, ErrorMessage = "图标路径不能超过300个字符")]
        public string IconPicture { get; set; }

        [StringLength(300, ErrorMessage = "频道描述不能超过300个字")]
        public string Description { get; set; }

        public bool IsActive { get; set; }

        public int Hits { get; set; }

        public int ParentId { get; set; }

        public int Sort { get; set; }

        /// <summary>
        /// // 1:单页 2:标题列表 3:图片列表 4:表单页
        /// </summary>
        public int ChannelType { get; set; }

        [NotMapped]
        public bool IsCurrent { get; set; }

        [NotMapped]
        public virtual List<Channel> Childrens { get; set; }
    }
}
