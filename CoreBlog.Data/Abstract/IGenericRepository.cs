using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreBlog.Data.Abstract
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> Get(string uniqkey);
        Task<T> Get(Expression<Func<T, bool>> expression);
        Task<IQueryable<T>> GetAll();
        Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> expression);
        Task<T> Create(T entity);
        Task<T> Remove(string uniqkey);
        Task Update(T entity);
        Task SaveChanges();


    }
}
