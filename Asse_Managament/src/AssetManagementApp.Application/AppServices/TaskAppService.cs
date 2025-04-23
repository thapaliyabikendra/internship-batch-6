using AssetManagementApp.Dtos.TaskManagementDtos;
using AssetManagementApp.Entities;
using AssetManagementApp.Interfaces;
using AutoMapper.Internal.Mappers;
using Microsoft.Extensions.Logging;
using Polly.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AssetManagementApp.AppServices;

public class TaskAppService(IRepository<TaskItem, Guid> taskRepository, IPagedAndSortedResultRequest paged)
    : ApplicationService, ITaskAppService
{
    public async Task<TaskDto> CreateAsync(CreateUdateTaskDto input)
    {
        try
        {
            if (input.Title.IsNullOrWhiteSpace())
                throw new UserFriendlyException("Title cannot be empty");

            if (input.CategoryId == Guid.Empty)
                throw new UserFriendlyException("CategoryId is invalid");

            // Optional: Validate the category exists
            var categoryExists = await taskRepository.WithDetailsAsync(x => x.Category);
            var task = new TaskItem
            {
                Title = input.Title.Trim(),
                Description = input.Description?.Trim(),
                CategoryId = input.CategoryId,
            };

            var result = await taskRepository.InsertAsync(task, autoSave: true);

            return new TaskDto
            {
                Id = result.Id
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error occurred while creating a task.");
            throw new UserFriendlyException("Error while creating new task", ex.Message);
        }
    }

    public async Task<TaskDto> GetAsync(Guid id)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new UserFriendlyException("Id cannot be empty");
            }

            var task = await taskRepository.GetAsync(id);

            if(task == null)
            {
                throw new UserFriendlyException("Task not found");
            }

            var result = ObjectMapper.Map<TaskItem,TaskDto>(task);

            //var result = new TaskDto()
            //{
            //    Id = task.Id,
            //    Title = task.Title,
            //    Description = task.Description,
            //    IsCompleted = task.IsCompleted,
            //    CategoryId = task.CategoryId,
            //    Name = task.Name
            //};

            return result;
        }
        catch (Exception ex)
        {
            throw new UserFriendlyException("Error while creating new task", ex.Message);
        }

    }

    public async Task<PagedResultDto<TaskDto>> GetListAsync(string? categoryNameFilter, bool? isCompletedFilter, PagedAndSortedResultRequestDto input)
    {
        if(input.MaxResultCount <= 0)
        {
            throw new UserFriendlyException("MaxResultCount must be greater than 0");
        }

        if (input.SkipCount < 0)
        {
            throw new UserFriendlyException("SkipCount must be greater than or equal to 0");
        }

        var result = await taskRepository.GetListAsync();

        var filteredResult = result.WhereIf(!categoryNameFilter.IsNullOrWhiteSpace(), x => x.Category.Name.Contains(categoryNameFilter))
            .WhereIf(isCompletedFilter.HasValue, x => x.IsCompleted == isCompletedFilter.Value)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .ToList();

        var totalCount = result.Count();

        var pagedResult = new PagedResultDto<TaskDto>()
        {
            TotalCount = totalCount,
            Items = filteredResult.Select(x => new TaskDto()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                IsCompleted = x.IsCompleted,
                CategoryId = x.CategoryId,
                Name = x.Category.Name
            }).ToList()
        };

        return pagedResult;
    }
}
