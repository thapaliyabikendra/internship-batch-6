using AssetManagementApp.Dtos.TaskManagementDtos;
using AssetManagementApp.Entities;
using AssetManagementApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AssetManagementApp.AppServices;

public class CategotyAppService(IRepository<Category, Guid> categoryRepository)
    : ApplicationService, ICategoryAppService
{
    public async Task<CategoryDto> CreateAsync(CategoryDto input)
    {
        if (input.Name.IsNullOrWhiteSpace())
        {
            throw new UserFriendlyException("Name cannot be empty");
        }

        // logic for creating a category
        var categoryData = new Category()
        {
            Name = input.Name.Trim()
        };

        var result = await categoryRepository.InsertAsync(categoryData);

        var returnCategory = new CategoryDto()
        {
            Id = result.Id
        };

        return returnCategory;

    }

    public async Task<List<CategoryDto>> GetListAsync()
    {
        if(categoryRepository == null)
        {
            throw new UserFriendlyException("Category repository is Empty!");
        }

        var categories = await categoryRepository.GetListAsync();

        var categoryList = categories.Select(x => new CategoryDto
        {
            Id = x.Id,
            Name = x.Name
        }).ToList();

        return categoryList;
    }
}
