using BLL.Models;
using DAL.Entities;

namespace BLL.Interfaces;

public interface IBlogService
{
    Task<ServiceResponse<Category>> AddCategoryAsync(Category entity);
}