using AssetManagementApp.AssetsDtos;
using AssetManagementApp.DepartmentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AssetManagementApp.Interface;

public interface IDepartmentAppService : IApplicationService
{
    Task<CreateDepartmentResponseDto> CreateAsync(CreateDepartmentRequestDto input);
    Task<DepartmentResponseDto> GetAsync(Guid Id);
    Task<List<DepartmentResponseDto>> GetListAsync();

    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(Guid id, UpdateDepartmentDto input);
}
