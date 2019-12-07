using CoreBlog.Entity.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreBlog.Web.ViewModels
{
    public class PagingInfo
    {
        public int Total { get; set; }
        public int ItemPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CategoryName { get; set; }
        public int TotalPages()
        {
            int tp;
            tp = (int)Math.Ceiling((decimal)Total / ItemPerPage);
            return tp;
        }
    }
    public class PostList
    {
        public List<Post> Posts { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    
}
