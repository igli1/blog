﻿using BLL.Interfaces;
using BLL.Models;
using DAL;
using DAL.Entities;

namespace BLL.Services;

public class BlogService : IBlogService
{
    private readonly UnitOfWork _unitOfWork;

    public BlogService(UnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<ServiceResponse<Category>> AddCategoryAsync(Category entity)
    {
        var serviceResponse = new ServiceResponse<Category>();
        try
        {
            await _unitOfWork.Categories.AddAsync(entity);
            await _unitOfWork.CommitAsync();
            
            serviceResponse.Status = true;
            serviceResponse.Data = entity;
            return serviceResponse;
        }
        catch (Exception e)
        {
            serviceResponse.Status = false;
            serviceResponse.Message = e.Message;
            return serviceResponse;
        }
    }

    public async Task<ServiceResponse<Category>> GetCategoryAsync(Guid categoryId)
    {
        var serviceResponse = new ServiceResponse<Category>();
        
        var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
        
        serviceResponse.Status = true;
        serviceResponse.Data = category;
        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> DeleteCategoryAsync(Guid categoryId)
    {
        var serviceResponse = new ServiceResponse<bool>();
        
        var category = await _unitOfWork.Categories.GetByIdAsync(categoryId);
        serviceResponse.Status = true;

        if (category is null)
        {
            serviceResponse.Data = false;
            serviceResponse.Message = "Category not found";
            
            return serviceResponse;
        }

        var postCategoriesQuery =  _unitOfWork.PostCategories.GetAll();
        
        var postCategoriesCount = postCategoriesQuery.Where(pc => pc.CategoryGuid == categoryId)?.Count();

        if (postCategoriesCount > 0)
        {
            serviceResponse.Data = false;
            serviceResponse.Message = "Category has blog post's and can't be deleted";
            
            return serviceResponse;
        }
        
        _unitOfWork.Categories.Remove(category);
        await _unitOfWork.CommitAsync();
        
        serviceResponse.Data = true;
        return serviceResponse;
    }

}