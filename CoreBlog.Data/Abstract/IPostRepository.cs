using CoreBlog.Entity.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBlog.Data.Abstract
{
    public interface IPostRepository :IGenericRepository<Post>
    {
        Task<IQueryable<Post>> GetWithCategory();
        Task PassOrActive(string uniqkey);
        Task<Post> GetPost(string url);
    }
}
