using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace IBLL.Model.Cms
{
    [Serializable]
    [Table("Cms_Article")]
    public class Article : ModelBase
    {
        [StringLength(100, ErrorMessage = "文章标题不能超过100个字")]
        [Required(ErrorMessage = "文章标题不能为空")]
        public string Title { get; set; }

        [StringLength(300, ErrorMessage = "文章图片路径不能超过300个字符")]
        public string CoverPicture { get; set; }

        [StringLength(300, ErrorMessage = "缩略图路径不能超过300个字符")]
        public string InnerPicture { get; set; }

        [Required(ErrorMessage = "文章内容不能为空")]
        [StringLength(int.MaxValue)]
        public string Content { get; set; }

        public int Hits { get; set; }

        public int Diggs { get; set; }

        public bool IsActive { get; set; }

        public string File { get; set; }

        public bool IsProposed { get; set; }

        [Required(ErrorMessage = "请选择文章所属频道")]
        public int ChannelId { get; set; }

        [ForeignKey("ChannelId")]
        public virtual Channel Channel { get; set; }

        public virtual List<Tag> Tags { get; set; }

        [NotMapped]
        public string TagString
        {
            get
            {
                return Tags != null ? string.Join(",", Tags.Select(t => t.Name)) : string.Empty;
            }
            set
            {
                this.Tags = !string.IsNullOrWhiteSpace(value)
                    ? value.Split(',').Select(t => new Tag() { Name = t }).ToList()
                    : new List<Tag>();
            }
        }
    }
}
