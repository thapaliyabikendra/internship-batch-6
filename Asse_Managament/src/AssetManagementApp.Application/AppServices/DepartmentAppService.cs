using AssetManagementApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using AssetManagementApp.Assets;
using Microsoft.Extensions.Logging;
using System.Reflection.Metadata.Ecma335;
using AssetManagementApp.Dtos;
using Volo.Abp;
using AssetManagementApp.Dtos.DepartmentDtos;
using AssetManagementApp.Permissions;
using Microsoft.AspNetCore.Authorization;

namespace AssetManagementApp.AppServices;
[Authorize(AssetManagementAppPermissions.Assets.Default)]

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
                throw new Exception("DepartmentName cannot be Empty!");
            }
            if (input.DepartmentSystemName.IsNullOrWhiteSpace())
            {
                throw new Exception("DepartmentSystemName cannot be Empty");
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

    public async Task<IEnumerable<GetDepartmentDto>> GetAllAsync()
    {
        try
        {
            if(departmentRepository == null)
            {
                throw new Exception("Department repository is Empty!");
            }
            logger.LogDebug("Starting Department App Service");
            logger.LogInformation("Retrieving all departments");

            var getDepartments = await departmentRepository.GetListAsync();

            var departments = getDepartments.Select(x => new GetDepartmentDto
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

    public async Task<GetDepartmentDto> GetByIdAsync(Guid id)
    {
        try
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

            var departmentDto = new GetDepartmentDto
            {
                DepartmentName = department.DepartmentName,
                DepartmentSystemName = department.DepartmentSystemName,
                IsActive = department.IsActive,
                Description = department.Description
            };

            return departmentDto;

        }
        catch (Exception ex)
        {
            throw new UserFriendlyException("Exceptions");
        }

        
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateDepartmentDto input)
    {
        try
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("Id cannot be empty", nameof(id));
            }

            if (input.DepartmentName.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("DepartmentName cannot be Emptys or whitespace");
            }

            if (input.DepartmentSystemName.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("DepartmentSystemName cannot be Empty or whitespace");
            }

            var department = await departmentRepository.GetAsync(id);

            if (department == null)
            {
                throw new Exception($"Department with ID {id} not found.");
            }

            department.DepartmentName = input.DepartmentName.Trim();
            department.DepartmentSystemName = input.DepartmentSystemName.Trim().ToUpper();
            department.IsActive = input.IsActive;
            department.Description = input.Description?.Trim();

            await departmentRepository.UpdateAsync(department);

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating department");
            throw;
        }


    }
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting department");
            throw;
        }
    }
}
