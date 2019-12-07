using CoreBlog.Business.Abstract;
using CoreBlog.Data.Abstract;
using CoreBlog.Entity.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreBlog.Business.Concrete
{
    public class PostManager: GenericManager<Post>, IPostService
    {
        private readonly IPostRepository postRepository;

        public PostManager(IPostRepository postRepository):base(postRepository)
        {
            this.postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
        }

        public async Task<Post> GetPost(string url)
        {
            return await postRepository.GetPost(url);
        }

        public async Task<IQueryable<Post>> GetWithCategory()
        {
            return await postRepository.GetWithCategory();
        }
        public async Task PassOrActive(string uniqkey)
        {
            await postRepository.PassOrActive(uniqkey);
        }
    }
}
