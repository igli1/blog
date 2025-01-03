namespace DAL.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IRefreshTokensRepository RefreshTokens { get;  }
    IUserRepository Users { get;  }
    Task CommitAsync();
}