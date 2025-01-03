namespace DAL.Interfaces;

public interface IUnitOfWork : IAsyncDisposable
{
    IRefreshTokensRepository RefreshTokens { get;  }
    IUserRepository Users { get;  }
    ICategoryRepository Categories { get;  }
    IPostCategoryRepository PostCategories { get;  }
    IPostRepository Post { get;  }
    Task CommitAsync();
}