namespace DAL.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IRefreshTokensRepository RefreshTokens { get;  }
    IUserRepository Users { get;  }
    ICategoryRepository Categories { get;  }
    Task CommitAsync();
}