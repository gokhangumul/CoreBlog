using System.Linq;
using System.Threading.Tasks;
using CoreBlog.Data.Abstract;
using CoreBlog.Data.Concrete.EfCore.Context;
using CoreBlog.Entity.DbModel;
using Microsoft.EntityFrameworkCore;

namespace CoreBlog.Data.Concrete.EfCore.Repository
{
    public class PostRepository : GenericRepository<Post>, IPostRepository
    {
        public PostRepository(BlogContext blogContext) : base(blogContext)
        {
        }

        public async Task<Post> GetPost(string url)
        {
            var result= await blogContext.Posts.FirstOrDefaultAsync(x => x.Url == url);
            return result;
        }

        public async Task<IQueryable<Post>> GetWithCategory()
        {
            var result = await blogContext.Posts.Include(x => x.Category).ToListAsync();
            return result.AsQueryable();
        }
        public async Task PassOrActive(string uniqkey)
        {
            var post = await blogContext.Posts.FirstOrDefaultAsync(x => x.UniqKey == uniqkey);
            if (post.IsActive == true)
            {
                post.IsActive = false;
            }
            else
            {
                post.IsActive = true;
            }
           await blogContext.SaveChangesAsync();
        }
    }
}
