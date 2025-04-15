using AssetManagementApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManagementApp.DepartmentDtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using AssetManagementApp.Assets;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;
using AssetManagementApp.AssetsDtos;
using Volo.Abp;

namespace AssetManagementApp.AppServices;

public class DepartmentAppService(ILogger<DepartmentAppService> logger, 
    IRepository<Department, Guid> departmentRepository) 
    : ApplicationService, IDepartmentAppService
{

    public async Task<CreateDepartmentResponseDto> CreateAsync(CreateDepartmentDto input)
    {
        try
        {
            ILogger logger = new LoggerFactory().CreateLogger<DepartmentAppService>();
            logger.LogInformation("Creating department with name: {DepartmentName}", input.DepartmentName);

            if (input.DepartmentName.IsNullOrWhiteSpace())
            {
                throw new Exception("DepartmentName cannot be null");
            }
            if (input.DepartmentSystemName.IsNullOrWhiteSpace())
            {
                throw new Exception("DepartmentSystemName cannot be null");
            }
            var department = new Department()
            {
                DepartmentName = input.DepartmentName.Trim(),
                DepartmentSystemName = input.DepartmentSystemName.Trim().ToUpper(),
                IsActive = input.IsActive,
                Description = input.Description?.Trim()
            };
            await departmentRepository.InsertAsync(department);

            var result = new CreateDepartmentResponseDto()
            {
                Id = department.Id
            };

            return result;

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating department");
            throw;
        }

    }

    public async Task<IEnumerable<CreateDepartmentDto>> GetAllDepartmentAsync()
    {
        try
        {
            if(departmentRepository == null)
            {
                throw new Exception("Department repository is null");
            }
            var getDepartments = await departmentRepository.GetListAsync();

            var departments = getDepartments.Select(x => new CreateDepartmentDto
            {
                DepartmentName = x.DepartmentName,
                DepartmentSystemName = x.DepartmentSystemName,
                IsActive = x.IsActive,
                Description = x.Description
            });

            return departments;

        }
        catch(Exception ex)
        {
            logger.LogError(ex, "Error retrieving departments");
            throw;
        }

    }

    public async Task<CreateDepartmentResponseDto> GetDepartmentAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty", nameof(id));
        }

        var department = await departmentRepository.GetAsync(id);

        if (department == null)
        {
            throw new UserFriendlyException("Department not found.");
        }

        var departmentDto = new CreateDepartmentResponseDto
        {
            DepartmentName = department.DepartmentName,
            DepartmentSystemName = department.DepartmentSystemName,
            IsActive = department.IsActive,
            Description = department.Description
        };

        return departmentDto;

    }

    public async Task<bool> UpdateDepartmentAsync(Guid id, CreateDepartmentDto input)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty", nameof(id));
            }

            if (input.DepartmentName.IsNullOrWhiteSpace())
            {
                throw new Exception("DepartmentName cannot be null");
            }
            if (input.DepartmentSystemName.IsNullOrWhiteSpace())
            {
                throw new Exception("DepartmentSystemName cannot be null");
            }

            var department = await departmentRepository.GetAsync(id);
            if (department == null)
            {
                throw new Exception("Department not found");
            }

            var result = await departmentRepository.UpdateAsync(department);

            result.DepartmentName = input.DepartmentName.Trim();
            result.DepartmentSystemName = input.DepartmentSystemName.Trim().ToUpper();
            result.IsActive = input.IsActive;
            result.Description = input.Description?.Trim();

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating department");
            throw;
        }

    }
    public async Task<bool> DeleteDepartmentAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("Id cannot be empty", nameof(id));
        }

        var department = await departmentRepository.GetAsync(id);

        if (department == null)
        {
            throw new Exception("Department not found");
        }

        await departmentRepository.DeleteAsync(department);

        return true;
    }

}
