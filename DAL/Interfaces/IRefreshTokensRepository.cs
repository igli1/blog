using DAL.Entities;

namespace DAL.Interfaces;

public interface IRefreshTokensRepository : IRepository<RefreshTokens>
{
    Task<RefreshTokens> GetByToken(string token);
    RefreshTokens UpdateToken(RefreshTokens entity);
}