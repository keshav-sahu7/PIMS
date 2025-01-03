namespace PIMS.Repository;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(string id, CancellationToken cancellationToken = default);
    IQueryable<TEntity> GetAll();
    void Add(TEntity entity);
    void AddMultiple(IEnumerable<TEntity> entities);
    void Update(TEntity entity);
    void UpdateMultiple(IEnumerable<TEntity> entities);
    void Delete(TEntity entity);
}