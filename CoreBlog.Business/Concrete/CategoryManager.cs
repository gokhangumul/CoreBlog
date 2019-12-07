using CoreBlog.Business.Abstract;
using CoreBlog.Data.Abstract;
using CoreBlog.Entity.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreBlog.Business.Concrete
{
    public class CategoryManager :GenericManager<Category>,ICategoryService
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryManager(ICategoryRepository categoryRepository) :base(categoryRepository)
        {
            this.categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        }

        public async Task<IQueryable> GetAllWithPost(Expression<Func<Category, bool>> expression)
        {
            return await categoryRepository.GetAllWithPost(expression);
        }
    }
}
