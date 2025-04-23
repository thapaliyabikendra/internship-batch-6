using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.Dtos;
using Volo.Abp.Application.Services;

namespace TaskManagement.Interface;

public interface ICategoryAppService : IApplicationService
{
    Task<CreateCategoryResponseDto> CreateAsync(CategoryDto input);
    Task<List<CategoryListResponseDto>> GetListAsync();
}
