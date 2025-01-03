using DAL.Entities;

namespace DAL.Interfaces;

public interface IRefreshTokensRepository : IRepository<RefreshTokens>
{
    RefreshTokens GetByToken(string token);
}