namespace DAL.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> GetByIdAsync(Guid id);
    IQueryable<T> GetAll();
    Task AddAsync(T entity);
    void Remove(T entity);
}