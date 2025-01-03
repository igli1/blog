using DAL.Interfaces;
using DAL.Repositories;

namespace DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly BlogContext _context;
    public UnitOfWork(BlogContext context)
    {
        _context = context;
        RefreshTokens = new RefreshTokensRepository(_context);
        Users = new UserRepository(_context);
    }
    public IRefreshTokensRepository RefreshTokens { get; private set; }
    public IUserRepository Users { get;  private set; }


    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}