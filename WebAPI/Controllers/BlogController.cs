﻿using BLL.Interfaces;
using DAL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Dtos;
using WebAPI.Dtos.Blog;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogController : ControllerBase
{
    private readonly IBlogService _blogService;
    public BlogController(IBlogService blogService)
    {
        _blogService = blogService;
    }

    [HttpPost]
    [Authorize (Roles = "Admin")]
    public async Task<ActionResult> AddCategory([FromBody] CategoryDto model)
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

        var category = new Category
        {
            Name = model.CategoryName.ToLower()
        };
        
        var categoryResponse = await _blogService.AddCategoryAsync(category);

        if (!categoryResponse.Status)
        {
            var error = new ErrorDto
            {
                Code = "Error",
                Description = categoryResponse.Message
            };
            return BadRequest(error);
        }
        
        return Ok(categoryResponse.Data);
    }
}