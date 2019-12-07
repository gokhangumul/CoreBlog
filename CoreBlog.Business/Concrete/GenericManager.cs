using CoreBlog.Business.Abstract;
using CoreBlog.Data.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CoreBlog.Business.Concrete
{
    public class GenericManager<T> : IService<T> where T : class
    {
        private readonly IGenericRepository<T> genericRepository;

        public GenericManager(IGenericRepository<T> genericRepository)
        {
            this.genericRepository = genericRepository ?? throw new ArgumentNullException(nameof(genericRepository));
        }

        public async Task<T> Create(T entity)
        {
            return await genericRepository.Create(entity);
        }

        public async Task<T> Get(string uniqkey)
        {
            return await genericRepository.Get(uniqkey);
        }

        public async  Task<T> Get(Expression<Func<T, bool>> expression)
        {
            return await genericRepository.Get(expression);
        }

        public async Task<IQueryable<T>> GetAll()
        {
            return await genericRepository.GetAll();
        }

        public async  Task<IQueryable<T>> GetAll(Expression<Func<T, bool>> expression)
        {
            return await genericRepository.GetAll(expression);
        }

        public Task<T> Remove(string uniqkey)
        {
            throw new NotImplementedException();
        }

        public async Task SaveChanges()
        {
           await genericRepository.SaveChanges();
        }

        public async Task Update(T entity)
        {
           await genericRepository.Update(entity);
        }
    }
}
