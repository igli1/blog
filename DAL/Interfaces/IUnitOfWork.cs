namespace DAL.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IRefreshTokensRepository RefreshTokens { get;  }
    Task CommitAsync();
}