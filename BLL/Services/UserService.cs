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
    
    public async Task<ServiceResponse<RefreshTokens>> AddRefreshTokens(RefreshTokens rt)
    {
        var serviceResponse = new ServiceResponse<RefreshTokens>();
        try
        {
            var existingRt =  _unitOfWork.RefreshTokens.GetByToken(rt.RefreshToken);

            if (existingRt != null)
            {
                var refreshTokenResponse = GenerateRefreshToken();
                rt.RefreshToken = refreshTokenResponse.RefreshToken;
                rt.RefreshTokenExpiryTime = refreshTokenResponse.RefreshTokenExpiryTime;
            }
            
            rt.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwt.RefreshTokenValidityInDays);
            await _unitOfWork.RefreshTokens.AddAsync(rt);
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
    public AccessTokenResponse GenerateAccessToken(string email, string userRole, Guid userId)
    {
        var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwt.Key));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, userId.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(ClaimTypes.Role, userRole),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_jwt.TokenValidityInMinutes),
            signingCredentials: credentials);

        var tokenResponse = new AccessTokenResponse
        {
            AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
            AccessTokenExpiryTime = token.ValidTo
        };
        
        return tokenResponse;
    }
    public RefreshTokenResponse GenerateRefreshToken()
    {
        var randomNumber = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);

        var tokenResponse = new RefreshTokenResponse
        {
            RefreshToken = Convert.ToBase64String(randomNumber),
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwt.RefreshTokenValidityInDays)
        };
        
        return tokenResponse;
    }
    public async Task<ServiceResponse<TokensResponse>> RefreshAccessToken(string refreshToken)
    {
        var serviceResponse = new ServiceResponse<TokensResponse>();

        try
        {
            serviceResponse.Status = true;
            var existingRt =  _unitOfWork.RefreshTokens.GetByToken(refreshToken);

            if (existingRt is null || existingRt.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "Refresh token does not exist or is expired ";
                return serviceResponse;
            }

            var user = _unitOfWork.Users.GetUserWithRoles(existingRt.UserId);

            if (user == null)
            {
                serviceResponse.Data = null;
                serviceResponse.Message = "User does not exist";
                return serviceResponse;
            }
            var tokenResponse = new TokensResponse
            {
                AccessToken = GenerateAccessToken(user.Email,  user?.RoleName, user.UserId),
                RefreshToken = new RefreshTokenResponse
                {
                    RefreshTokenExpiryTime = existingRt.RefreshTokenExpiryTime,
                    RefreshToken = existingRt.RefreshToken
                }
            };
            serviceResponse.Data = tokenResponse;
            
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Status = false;
            serviceResponse.Message = e.Message;
            return serviceResponse;
        }

    }

}