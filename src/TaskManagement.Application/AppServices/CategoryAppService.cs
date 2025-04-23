using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Dtos;
using TaskManagement.Entities;
using TaskManagement.Interface;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;

namespace TaskManagement.AppServices;

public class CategoryAppService : ApplicationService, ICategoryAppService
{
    private readonly IRepository<Category, Guid> _categoryRepository;

    public CategoryAppService(IRepository<Category, Guid> categoryRepository)
    {
        _categoryRepository = categoryRepository;
    }

    public async Task<CategoryDto> CreateAsync(CategoryDto input)
    {
        var existingCategory = await _categoryRepository.FirstOrDefaultAsync(c => c.Name == input.Name);
        if (existingCategory != null)
        {
            throw new UserFriendlyException("Category with this name already exists.");
        }

        var category = ObjectMapper.Map<CategoryDto,Category>(input);
        await _categoryRepository.InsertAsync(category);

        return ObjectMapper.Map<Category,CategoryDto>(category);
    }

    public async Task<List<CategoryDto>> GetListAsync()
    {
        var categories = await _categoryRepository.GetListAsync();
        return ObjectMapper.Map<List< Category>,List<CategoryDto>>(categories);
    }
}
