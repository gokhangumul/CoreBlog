using CoreBlog.Entity.DbModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreBlog.Business.Abstract
{
    public interface ICategoryService:IService<Category>
    {
        Task<IQueryable> GetAllWithPost(Expression<Func<Category, bool>> expression);
    }
}
