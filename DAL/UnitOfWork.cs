using DAL.Interfaces;
using DAL.Repositories;

namespace DAL;

public class UnitOfWork : IUnitOfWork
{
    private readonly BlogContext _context;
    public IRefreshTokensRepository RefreshTokens { get; }

    public UnitOfWork(BlogContext context)
    {
        _context = context;
        RefreshTokens = new RefreshTokensRepository(_context);
    }

    public async Task CommitAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async ValueTask DisposeAsync()
    {
        await _context.DisposeAsync();
    }
}