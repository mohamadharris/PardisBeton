using Repository.EF.Base;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.EF.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IBaseRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class;

        TRepository GetInheritedRepository<TRepository, TEntity, TKey>() where TRepository : class, IBaseRepository<TEntity, TKey> where TEntity : class;
        Task<int> CompleteAsync();
        int Complete();
    }
}

