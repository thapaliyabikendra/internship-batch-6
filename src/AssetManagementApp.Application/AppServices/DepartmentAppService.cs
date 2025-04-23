using AssetManagementApp.DepartmentDtos;
using AssetManagementApp.Entities;
using AssetManagementApp.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Volo.Abp.Caching;

namespace AssetManagementApp.AppServices
{
    public class DepartmentAppService(
        IRepository<Department, Guid> departmentRepository,
        IDistributedCache<List<DepartmentResponseDto>> _cache)
        : ApplicationService, IDepartmentAppService
    {
        private const string CacheKey = "department-list"; // Use a specific cache key for departments

        // Create Logic
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

                // Invalidate cache after creation
                await _cache.RemoveAsync(CacheKey);

                var result = new CreateDepartmentResponseDto()
                {
                    Id = department.Id
                };

                return result;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error creating Department");
                throw new Exception("Error creating Department", ex);
            }
        }

        // Delete Logic
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var department = await departmentRepository.AnyAsync(a => a.Id == id);
                if (!department)
                {
                    throw new Exception("Department not found.");
                }

                await departmentRepository.DeleteAsync(id);

                // Invalidate cache after deletion
                await _cache.RemoveAsync(CacheKey);

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error deleting department");
                throw new Exception("Error deleting department", ex);
            }
        }

        // Get by ID Logic
        public async Task<DepartmentResponseDto> GetAsync(Guid id)
        {
            try
            {
                var department = await departmentRepository.FindAsync(id);
                if (department is null)
                {
                    throw new UserFriendlyException("Department not found.");
                }

                return new DepartmentResponseDto()
                {
                    Id = department.Id,
                    DisplayName = department.DisplayName,
                    IsActive = department.IsActive,
                    Description = department.Description
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error retrieving department");
                throw new UserFriendlyException("Error retrieving department", "500");
            }
        }

        // Get List Logic with Cache
        public async Task<List<DepartmentResponseDto>> GetListAsync()
        {
            try
            {
                var cachedData = await _cache.GetAsync(CacheKey);
                if (cachedData != null)
                {
                    return cachedData;
                }

                var departments = await (await departmentRepository.GetQueryableAsync())
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .Select(y => new DepartmentResponseDto
                    {
                        Id = y.Id,
                        DisplayName = y.DisplayName,
                        SystemName = y.SystemName,
                        IsActive = y.IsActive,
                        Description = y.Description
                    })
                    .ToListAsync(); // Use ToListAsync() for async execution

                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) // Cache for 1 hour
                };

                // Save the fetched data into the cache
                await _cache.SetAsync(CacheKey, departments, cacheOptions);

                return departments;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error retrieving departments");
                throw new UserFriendlyException("Error retrieving departments", "500");
            }
        }

        // Update Logic
        public async Task<bool> UpdateAsync(Guid id, UpdateDepartmentDto input)
        {
            try
            {
                var department = await departmentRepository.GetAsync(id);
                if (department == null)
                {
                    throw new UserFriendlyException("Department not found.");
                }

                // Validate input
                if (input.SystemName.IsNullOrWhiteSpace())
                {
                    throw new UserFriendlyException("SystemName cannot be null");
                }
                if (input.DisplayName.IsNullOrWhiteSpace())
                {
                    throw new UserFriendlyException("DisplayName cannot be null");
                }

                // Update department details
                department.DisplayName = input.DisplayName.Trim();
                department.SystemName = input.SystemName.Trim().ToUpper();
                department.IsActive = input.IsActive;
                department.Description = input.Description?.Trim();

                await departmentRepository.UpdateAsync(department);

                // Invalidate cache after update
                await _cache.RemoveAsync(CacheKey);

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error updating Department");
                throw new UserFriendlyException("Error updating department", "500");
            }
        }
    }
}
