using AssetManagementApp.Dtos.TaskManagementDtos;
using AssetManagementApp.Interfaces;
using Polly.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AssetManagementApp.AppServices;

public class TaskAppService(IRepository taskRepository, IPagedAndSortedResultRequest paged)
    : ITaskAppService, IApplicationService
{
    public async Task<TaskDto> CreateAsync(CreateUdateTaskDto input)
    {
        try
        {
            if (input.Title.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("Title cannot be empty");
            }

            var result = new TaskDto()
            {
                Title = input.Title.Trim(),
                Description = input.Description?.Trim(),
                CategoryId = input.CategoryId


            };


        }
        catch (Exception ex)
        {
            throw new UserFriendlyException("Error while  creating new task", ex.Message);
        }
    }

    public Task<TaskDto> GetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<PagedResultDto<TaskDto>> GetListAsync(string? categoryNameFilter, bool? isCompletedFilter, PagedAndSortedResultRequestDto input)
    {
        throw new NotImplementedException();
    }
}
