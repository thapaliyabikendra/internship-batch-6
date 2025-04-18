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
    Task<IEnumerable<CreateDepartmentDto>> GetAllDepartmentAsync();
    Task<CreateDepartmentResponseDto> GetDepartmentAsync(Guid id);
    Task<bool> DeleteDepartmentAsync(Guid id);
    Task<bool> UpdateDepartmentAsync(Guid id, UpdateDepartmentDto input);
}
