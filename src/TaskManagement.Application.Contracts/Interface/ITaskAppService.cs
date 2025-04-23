using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace TaskManagement.Interface;

public interface ITaskAppService : IApplicationService
{
    Task<TaskDto> CreateAsync(CreateUpdateTaskDto input);
    Task<PagedResultDto<TaskDto>> GetListAsync(string? categoryNameFilter, bool? isCompletedFilter, PagedAndSortedResultRequestDto input);
    Task<TaskDto> GetAsync(Guid id);
}
