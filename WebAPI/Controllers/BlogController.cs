using System.Security.Claims;
using BLL.Interfaces;
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
    [Route("AddCategory")]
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
            Name = model.CategoryName.ToLower(),
            Description = model.CategoryDescription.ToLower(),
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
    
    [HttpGet]
    [Route("GetCategories/{categoryId}")]
    [Authorize]
    public async Task<ActionResult> AddCategory(Guid categoryId)
    {
        var categoryResponse = await _blogService.GetCategoryAsync(categoryId);
        
        return Ok(categoryResponse.Data);
    }
    [HttpDelete]
    [Route("DeleteCategory/{categoryId}")]
    [Authorize (Roles = "Admin")]
    public async Task<ActionResult> DeleteCategory(Guid categoryId)
    {
        var categoryResponse = await _blogService.DeleteCategoryAsync(categoryId);

        if (!categoryResponse.Status || !categoryResponse.Data)
        {
            var error = new ErrorDto
            {
                Code = "Error",
                Description = categoryResponse.Message
            };
            return BadRequest(error);
        }
        
        return Ok(new
        {
            Message = "Category deleted successfully",
            Statur = categoryResponse.Data
        });
    }
    
    [HttpGet]
    [Route("GetAllCategories")]
    [Authorize]
    public async Task<ActionResult> AddCategory()
    {
        var categoryResponse = await _blogService.GetAllCategoriesAsync();
        
        return Ok(categoryResponse.Data);
    }

    [HttpPut]
    [Route("EditCategory")]
    [Authorize (Roles = "Admin")]
    public async Task<ActionResult> EditCategory(EditCategoryDto model)
    {
        var category = new Category
        {
            Id = model.Id,
            Name = model.Name,
            Description = model.Description,
        };

        var categoryResponse = await  _blogService.UpdateCategoryAsync(category);

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

    [HttpPost]
    [Route("AddBlogPost")]
    [Authorize]
    public async Task<ActionResult> AddBlogPost([FromBody] PostDto model)
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
        
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        Guid userId = Guid.Parse(userIdClaim);
        
        var postCategory = new List<PostCategory>();
        
        foreach (var category in model.Categories)
        {
            postCategory.Add(new PostCategory
            {
                CategoryGuid = category
            });
        }

        var post = new Post
        {
            Title = model.Title,
            Content = model.Content,
            UserId = userId,
            PostCategories = postCategory,
            CreatedAt = DateTime.UtcNow,
            Status = model.Status,
            PublishAt = model.PublishAt,
            UpdatedAt =  DateTime.UtcNow,
        };
        
        var postResponse = await _blogService.AddPostAsync(post);

        if (!postResponse.Status)
        {
            var error = new ErrorDto
            {
                Code = "Error",
                Description = postResponse.Message
            };
            
            return BadRequest(error);
        }
        
        return Ok(postResponse.Data);
    }
}