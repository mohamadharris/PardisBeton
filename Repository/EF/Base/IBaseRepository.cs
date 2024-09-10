using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EF.Base
{
    public interface IBaseRepository<TEntity, TKey> where TEntity : class
    {

        // Asynchronous methods
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task<IEnumerable<TEntity>> FindAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity entity);

        // Synchronous methods
        IEnumerable<TEntity> GetAll();
        TEntity GetById(TKey id);
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);
        void Add(TEntity entity);

        // Common methods
        void Remove(TEntity entity);
        void Update(TEntity entity);



    }
}
