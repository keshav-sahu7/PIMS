using PIMS.Models;

namespace PIMS.Repository;

public class UnitOfWork : IUnitOfWork
{
    private readonly PimsContext _context;
    private readonly Dictionary<Type, object> _repositories;
    public UnitOfWork(PimsContext context)
    {
        _context = context;
        _repositories = new Dictionary<Type, object>();
    }

    public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
    {
        var entityType = typeof(TEntity);

        if (!_repositories.ContainsKey(entityType))
        {
            var repositoryInstance = new Repository<TEntity>(_context);
            _repositories[entityType] = repositoryInstance;
        }

        return (IRepository<TEntity>)_repositories[entityType];
    }

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
