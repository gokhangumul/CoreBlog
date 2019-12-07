using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreBlog.Entity.DbModel
{
    public class Image
    {
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Path { get; set; }
        [Required]
        public string GuiName { get; set; }
        [Required]
        public string UniqKey { get; set; }
        [Required]
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}
