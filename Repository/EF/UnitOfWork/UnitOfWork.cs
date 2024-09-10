using Microsoft.EntityFrameworkCore;
using Repository.EF.Base;
using Repository.EF.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbContext _context;
    private readonly Dictionary<Type, object> _repositories;

    public UnitOfWork(DbContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IBaseRepository<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : class
    {
        var type = typeof(TEntity);
        if (!_repositories.ContainsKey(type))
        {
            var repositoryType = typeof(BaseRepository<TEntity, TKey>);
            var repositoryInstance = Activator.CreateInstance(repositoryType, _context);
            _repositories.Add(type, repositoryInstance);
        }
        return (IBaseRepository<TEntity, TKey>)_repositories[type];
    }

    public TRepository GetInheritedRepository<TRepository, TEntity, TKey>()
 where TRepository : class, IBaseRepository<TEntity, TKey>
 where TEntity : class
    {
        var repositoryType = typeof(TRepository);
        if (!_repositories.ContainsKey(repositoryType))
        {
            var repositoryInstance = (TRepository)Activator.CreateInstance(repositoryType, _context);
            _repositories.Add(repositoryType, repositoryInstance);
        }
        return (TRepository)_repositories[repositoryType];
    }
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public int Complete()
    {
        return _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
