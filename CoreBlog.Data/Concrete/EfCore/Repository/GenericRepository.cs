using CoreBlog.Data.Abstract;
using CoreBlog.Data.Concrete.EfCore.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreBlog.Data.Concrete.EfCore.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        public readonly BlogContext blogContext;
        private readonly DbSet<T> entities;
        public GenericRepository(BlogContext blogContext)
        {
            this.blogContext = blogContext ?? throw new ArgumentNullException(nameof(blogContext));
            entities = blogContext.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
             await entities.AddAsync(entity);  
             await SaveChanges();
             return entity;
        }

        public Task<T> Get(string uniqkey)
        {
            throw new NotImplementedException();
        }

        public async Task<T> Get(Expression<Func<T, bool>> expression)
        {
            var result = await entities.FirstOrDefaultAsync(expression);
            return result;
        }

        public async Task<IQueryable<T>> GetAll()
        {
            var result = await entities.ToListAsync();
            return result.AsQueryable();
        }

        public async  Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> expression)
        {
            var result = await entities.Where(expression).ToListAsync();
            return result.AsQueryable();

        }

        public Task<T> Remove(string uniqkey)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChanges()
        {
            await blogContext.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
           blogContext.Entry(entity).State = EntityState.Modified;
           await SaveChanges();
        }
    }
}
