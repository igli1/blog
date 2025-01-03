using BLL.Models;
using DAL.Entities;

namespace BLL.Interfaces;

public interface IBlogService
{
    Task<ServiceResponse<Category>> AddCategoryAsync(Category entity);
    Task<ServiceResponse<Category>> GetCategoryAsync(Guid categoryId);
    Task<ServiceResponse<bool>> DeleteCategoryAsync(Guid categoryId);
    Task<ServiceResponse<IEnumerable<Category>>> GetAllCategoriesAsync();
    Task<ServiceResponse<Category>> UpdateCategoryAsync(Category entity);
}