using BLL.Configurations;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Dtos;
using WebAPI.Dtos.User;

namespace WebAPI.Controllers;
[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;
    private readonly JwtConfiguration _jwt;
    public UsersController(UserManager<User> userManager, IUserService userService, IOptions<JwtConfiguration> jwtConfiguration)
    {
        _userManager = userManager;
        _userService = userService;
        _jwt = jwtConfiguration.Value;
    }
    
    [HttpPost]
    [Route("login")]
    public async Task<ActionResult> Login([FromBody]LoginDto model)
    {
        if (!ModelState.IsValid)
        {

            var error = new ErrorDto
            {
                Code = "InvalidModel",
                Description = "The model is not valid."
            };
            return BadRequest(error);
        }

        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user == null)
        {
            
            var error = new ErrorDto
                {
                    Code = "UserDoesNotExist", 
                    Description = "User does not exist." 
                };
            return NotFound(error);
        }

        if (!await _userManager.CheckPasswordAsync(user, model.Password))
        {
                var error = new ErrorDto
                {
                    Code = "IncorrectPassword",
                    Description = "Incorrect Password!"
                };
            return Unauthorized(error);
        }
        
        var userRoles = await _userManager.GetRolesAsync(user);
        var accessTokenResponse = _userService.GenerateAccessToken(model.Email, userRoles.FirstOrDefault());
        
        var refreshTokenResponse = _userService.GenerateRefreshToken();

        var refeshTokenEntity = new RefreshTokens
        {
            UserId = user.Id,
            RefreshToken = refreshTokenResponse.RefreshToken,
            RefreshTokenExpiryTime = refreshTokenResponse.RefreshTokenExpiryTime
        };
        
        var insertRefreshTokenResponse = await _userService.AddRefreshTokens(refeshTokenEntity);

        if (!insertRefreshTokenResponse.Status)
        {
            var error = new ErrorDto
            {
                Code = "RefreshTokenNotSaved", 
                Description = insertRefreshTokenResponse.Message
            };
            return BadRequest(error);
        }
        
        var tokens = new TokenDto
        {
            AccessToken = accessTokenResponse.AccessToken,
            AccessTokenExpiryTime = accessTokenResponse.AccessTokenExpiryTime,
            RefreshToken = refreshTokenResponse.RefreshToken,
            RefreshTokenExpiryTime = refreshTokenResponse.RefreshTokenExpiryTime
        };
        
        return Ok(tokens);
    }
    
    [HttpPost]
    [Route("refresh-token")]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenDto model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorDto
            {
                Code = "InvalidModel",
                Description = "The model is not valid."
            };
            return BadRequest(error);
        }
        
        var tokenResponse = await _userService.RefreshAccessToken(model.RefreshToken);

        if (!tokenResponse.Status)
        {
            var error = new ErrorDto
            {
                Code = "Error",
                Description = tokenResponse.Message
            };
            return BadRequest(error);
        }
        
        var tokens = new TokenDto
        {
            AccessToken = tokenResponse.Data.AccessToken.AccessToken,
            AccessTokenExpiryTime = tokenResponse.Data.AccessToken.AccessTokenExpiryTime,
            RefreshToken = tokenResponse.Data.RefreshToken.RefreshToken,
            RefreshTokenExpiryTime = tokenResponse.Data.RefreshToken.RefreshTokenExpiryTime
        };
        
        return Ok(tokens);
        
    }

    [HttpPost]
    [Route("register")]
    public async Task<ActionResult> Login([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid)
        {
            var error = new ErrorDto
            {
                Code = "InvalidModel",
                Description = "The model is not valid."
            };
            return BadRequest(error);
        }
        
        var user = await _userManager.FindByEmailAsync(model.Email);

        if (user != null)
        { 
            var error = new ErrorDto
            {
                Code = "UserExists",
                Description = "A user with this email already exists."
            };
            
            return BadRequest(error);
        }
        var newUser = new User
        {
            UserName = model.Email,
            NormalizedUserName = model.Email.ToUpper(),
            Email = model.Email,
            NormalizedEmail = model.Email.ToUpper(),
            FirstName = model.FirstName.ToLower(),
            LastName = model.LastName.ToLower(),
        };
        
        var result = await _userManager.CreateAsync(newUser, model.Password);
        
        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => new ErrorDto
            {
                Code = e.Code,
                Description = e.Description
            }).ToList();
        
            return BadRequest(errors);
        }

        await _userManager.AddToRoleAsync(newUser, Role.User.ToString());

        return Ok(new
        {
            Message = "User registered successfully",
            Email = newUser.Email
        });
        
    }

    [HttpGet]
    [Route("test")]
    [Authorize]
    public ActionResult Get()
    {
        return Ok();
    }
}