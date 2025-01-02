﻿using BLL.Configurations;
using BLL.Interfaces;
using DAL.Entities;
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
        var accessToken = _userService.GenerateAccessToken(model.Email, userRoles.FirstOrDefault());
        
        var refreshToken = _userService.GenerateRefreshToken();
        var rt = new RefreshTokenDto
        {
            RefreshToken = refreshToken,
            RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(_jwt.RefreshTokenValidityInDays)
        };
        
        var tokens = new TokenDto
        {
            AccessToken = accessToken,
            RefreshToken = rt
        };
        
        return Ok(tokens);
    }
}