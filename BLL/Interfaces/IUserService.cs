using BLL.Models;
using DAL.Entities;

namespace BLL.Interfaces;

public interface IUserService
{
    Task<ServiceResponse<RefreshTokens>> AddRefreshTokens(RefreshTokens rt);
    string GenerateAccessToken(string email, string userRole);
    string GenerateRefreshToken();
}