using CoreBlog.Data.Abstract;
using CoreBlog.Data.Concrete.EfCore.Context;
using CoreBlog.Entity.DbModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreBlog.Data.Concrete.EfCore.Repository
{
    public class CategoryRepository : GenericRepository<Category>,ICategoryRepository
    {
        public CategoryRepository(BlogContext blogContext) : base(blogContext)
        {
        }

        public async Task<IQueryable> GetAllWithPost(Expression<Func<Category, bool>> expression)
        {
            var result = await blogContext.Categories.Include(x => x.Posts).ToListAsync();
            return result.AsQueryable();
        }
    }
}
