using DAL.Entities;
using DAL.Interfaces;
using DAL.Repositories;

namespace DAL;

public class UnitOfWork: IDisposable
{
    private readonly BlogContext _context;
    private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

    public IRepository<Post> PostRepository => GetRepository<Post>();
    public IRepository<Category> CategoryRepository => GetRepository<Category>();
    public IRepository<PostCategory> PostCategoryRepository => GetRepository<PostCategory>();

    public UnitOfWork(BlogContext context)
    {
        _context = context;
    }

    public void Commit()
    {
        _context.SaveChanges();
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    private IRepository<T> GetRepository<T>() where T : class
    {
        if (_repositories.ContainsKey(typeof(T)))
        {
            return (IRepository<T>)_repositories[typeof(T)];
        }

        var repository = new Repository<T>(_context.Set<T>());
        _repositories.Add(typeof(T), repository);
        return repository;
    }
}