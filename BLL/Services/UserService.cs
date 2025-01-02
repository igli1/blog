using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using BLL.Configurations;
using BLL.Interfaces;
using BLL.Models;
using DAL;
using DAL.Entities;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace BLL.Services;

public class UserService : IUserService
{
    private readonly UnitOfWork _unitOfWork;
    private readonly JwtConfiguration _jwt;

    public UserService(UnitOfWork unitOfWork, IOptions<JwtConfiguration> jwt)
    {
        _unitOfWork = unitOfWork;
        _jwt = jwt.Value;
    }
    
    public async Task<ServiceResponse<RefreshTokens>> AddOrUpdateRefreshTokens(RefreshTokens rt)
    {
        var serviceResponse = new ServiceResponse<RefreshTokens>();
        try
        {
            var existingRt = await _unitOfWork.RefreshTokens.GetByToken(rt.RefreshToken);

            if (existingRt == null)
            {
                existingRt.RefreshToken = GenerateRefreshToken();
                await _unitOfWork.RefreshTokens.AddAsync(rt);
            }
            else
            {
                existingRt.RefreshTokenExpiryTime = rt.RefreshTokenExpiryTime;
                _unitOfWork.RefreshTokens.UpdateToken(existingRt);
            }

            await _unitOfWork.CommitAsync();

            serviceResponse.Status = true;
            serviceResponse.Data = rt;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Status = false;
            serviceResponse.Message = e.Message;
            return serviceResponse;
        }
    }
    public string GenerateAccessToken(string email, string userRole)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwt.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, userRole),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.TokenValidityInMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}