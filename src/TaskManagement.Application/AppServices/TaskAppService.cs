using AutoMapper.Internal.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Dtos;
using TaskManagement.Entities;
using TaskManagement.Interface;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;
using Volo.Abp;
using Volo.Abp.ObjectMapping;

namespace TaskManagement.AppServices;

public class TaskAppService : ApplicationService, ITaskAppService
{
    private readonly IRepository<TaskItem, Guid> _taskRepository;
    private readonly IRepository<Category, Guid> _categoryRepository;

    public TaskAppService(IRepository<TaskItem, Guid> taskRepository, IRepository<Category, Guid> categoryRepository)
    {
        _taskRepository = taskRepository;
        _categoryRepository = categoryRepository;
    }

    public async Task<TaskDto> CreateAsync(CreateUpdateTaskDto input)
    {
        var taskItem = ObjectMapper.Map<CreateUpdateTaskDto, TaskItem>(input);

        await _taskRepository.InsertAsync(taskItem);

        return ObjectMapper.Map<TaskItem,TaskDto>(taskItem);
    }

    public async Task<PagedResultDto<TaskDto>> GetListAsync(string? categoryNameFilter, bool? isCompletedFilter, PagedAndSortedResultRequestDto input)
    {
        var query = await _taskRepository
     .GetQueryableAsync(); 

        if (!string.IsNullOrEmpty(categoryNameFilter))
        {
            query = query.Where(t => t.Category != null && t.Category.Name.Contains(categoryNameFilter));
        }

        if (isCompletedFilter.HasValue)
        {
            query = query.Where(t => t.IsCompleted == isCompletedFilter.Value);
        }

        var totalCount =  query.Count();

        var tasks =  query
            .OrderBy(t => t.Title)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount)
            .ToList();



        var taskDtos = ObjectMapper.Map<List<TaskItem>,List<TaskDto>>(tasks);

        return new PagedResultDto<TaskDto>(totalCount, taskDtos);
    }

    public async Task<TaskDto> GetAsync(Guid id)
    {
        var taskItem = await _taskRepository
            .FirstOrDefaultAsync(t => t.Id == id);

        if (taskItem == null)
        {
            throw new EntityNotFoundException(typeof(TaskItem), id);
        }

        return ObjectMapper.Map<TaskItem,TaskDto>(taskItem);
    }
}
