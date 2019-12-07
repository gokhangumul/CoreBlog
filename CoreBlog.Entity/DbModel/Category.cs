using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreBlog.Entity.DbModel
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Kategori adı boş geçilemez.")]
        public string Title { get; set; }
        public string UniqKey { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime? UpdatedTime { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public List<Post> Posts { get; set; }
    }
}
