using AssetManagementApp.Assets;
using AssetManagementApp.AssetsDtos;
using AssetManagementApp.DepartmentDtos;
using AssetManagementApp.Entities;
using AssetManagementApp.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AssetManagementApp.AppServices;

public class DepartmentAppService(
    IRepository <Department, Guid> departmentRepository) 
    : ApplicationService, IDepartmentAppService
{
    //Create Logic
    public async Task<CreateDepartmentResponseDto> CreateAsync(CreateDepartmentRequestDto input)
    {
		try
		{
            Logger.LogInformation("Creating department with name: {Department}", input.DisplayName);

            if (input.DisplayName.IsNullOrWhiteSpace())
			{
                throw new Exception("Display Name Cannot be Null");
            }

            var department = new Department()
            {
                DisplayName = input.DisplayName.Trim(),
                SystemName = input.SystemName.Trim().ToUpper(),
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
            Logger.LogError(ex, "Error creating Department");
            throw new Exception("Error creating Deepartment", ex);
        }

    }

    //Delete Logic
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var department = await departmentRepository.AnyAsync(a => a.Id == id);
            if (!department)
            {
                throw new Exception("Asset Category not found.");
            }

            await departmentRepository.DeleteAsync(id);
            return true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error retrieving or deleting department");
            throw new Exception("Error Reterving department", ex);
        }
    }

    //Read Logic
    public async Task<DepartmentResponseDto> GetAsync(Guid id)
    {
        try
        {
            var department = await departmentRepository.FindAsync(id);
            if (department is null)
            {
                throw new UserFriendlyException("Department not found.");
            }

            var result = new DepartmentResponseDto()
            {
                Id = department.Id,
                DisplayName = department.DisplayName,
                IsActive = department.IsActive,
                Description = department.Description
            };
            return result;
        }
        catch (UserFriendlyException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error retrieving department");
            throw new UserFriendlyException("Error Reterving department", "500");
        }
    }
    //Update Logic
    public async Task<bool> UpdateAsync(Guid id, UpdateDepartmentDto input)
    {
        try
        {
            var department = await departmentRepository.GetAsync(id);
            if (department == null)
            {
                throw new UserFriendlyException("Department not found.");
            }

            if (input.SystemName.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("SystemName cannot be null");
            }
            if (input.DisplayName.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("DisplayName cannot be null");
            }

            department.DisplayName = input.DisplayName.Trim();
            department.SystemName = input.SystemName.Trim().ToUpper();
            department.IsActive = input.IsActive;
            department.Description = input.Description?.Trim();

            await departmentRepository.UpdateAsync(department);

            return true;
        }
        catch (UserFriendlyException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error updating Departemnt");
            throw new UserFriendlyException("Department not found.");
        }
    }
}
