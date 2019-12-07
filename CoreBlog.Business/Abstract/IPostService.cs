using CoreBlog.Entity.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBlog.Business.Abstract
{
    public interface IPostService : IService<Post>
    {
        Task<IQueryable<Post>> GetWithCategory();
        Task PassOrActive(string uniqkey);
        Task<Post> GetPost(string url);
    }
}
