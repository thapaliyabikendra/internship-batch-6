using AssetManagementApp.Dtos;
using AssetManagementApp.Dtos.DepartmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AssetManagementApp.Interfaces;

public interface IDepartmentAppService : IApplicationService
{
    Task<CreateDepartmentResponseDto> CreateAsync(CreateDepartmentDto input);
    Task<IEnumerable<GetDepartmentDto>> GetAllAsync();
    Task<GetDepartmentDto>GetByIdAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> UpdateAsync(Guid id, UpdateDepartmentDto input);
}
