using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CoreBlog.Entity.DbModel
{
    public class Post
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Post başlığı boş geçilemez")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Post açıklaması boş geçilemez")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Post içeriği boş geçilemez")]
        public string PostContent { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdatedDate { get; set; }

        public string Url { get; set; }

        public string AuthorName { get; set; }
        public string PostDetailHeaderImage { get; set; }

        public int Hit { get; set; }
        [Required]
        public bool IsActive { get; set; }

        public string UniqKey { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
