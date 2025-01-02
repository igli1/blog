using BLL.Models;
using DAL.Migrations;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Interfaces;

public interface IUserService
{
    //Task<ServiceResponse<RefreshTokens>> AddOrUpdateRefreshTokens(RefreshTokens rt);
    string GenerateAccessToken(string email, string userRole);
    string GenerateRefreshToken();
}