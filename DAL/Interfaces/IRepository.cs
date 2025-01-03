namespace DAL.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(Guid id);
    IQueryable<TEntity> GetAll();
    Task AddAsync(TEntity entity);
    void Remove(TEntity entity);
}