using Microsoft.EntityFrameworkCore;
using PIMS.Models;

namespace PIMS.Repository;

public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
{
    private readonly PimsContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public Repository(PimsContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public async Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        return await _dbSet.FindAsync(new[] { id }, cancellationToken);
    }

    public IQueryable<TEntity> GetAll()
    {
        return _dbSet;
    }

    public void Add(TEntity entity)
    {
        _dbSet.Add(entity);
    }

    public void AddMultiple(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
            _dbSet.Add(entity);
    }

    public void Update(TEntity entity)
    {
        _dbSet.Update(entity);
    }

    public void UpdateMultiple(IEnumerable<TEntity> entities)
    {
        foreach (var entity in entities)
            _dbSet.Update(entity);
    }

    public void Delete(TEntity entity)
    {
        _dbSet.Remove(entity);
    }
}