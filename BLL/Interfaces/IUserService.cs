using BLL.Models;
using DAL.Entities;

namespace BLL.Interfaces;

public interface IUserService
{
    Task<ServiceResponse<RefreshTokens>> AddRefreshTokens(RefreshTokens rt);
    AccessTokenResponse GenerateAccessToken(string email, string userRole, Guid userId);
    RefreshTokenResponse GenerateRefreshToken();
    Task<ServiceResponse<TokensResponse>> RefreshAccessToken(string refreshToken);

}