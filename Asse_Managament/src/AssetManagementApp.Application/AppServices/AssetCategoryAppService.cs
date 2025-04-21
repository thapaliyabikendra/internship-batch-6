using AssetManagementApp.Assets;
using AssetManagementApp.Dtos.AssetsCategoryDtos;
using AssetManagementApp.Interfaces;
using AssetManagementApp.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Validation.Localization;

namespace AssetManagementApp.AppServices;
[Authorize(AssetManagementAppPermissions.Assets.Default)]

public class AssetCategoryAppService(ILogger<AssetCategoryAppService> logger,
    IRepository<AssetCategory, Guid> assetCategoryRepository, IDistributedCache cache)
    : ApplicationService, IAssetCategoryAppService
{
    public async Task<CreateAssetCategoryResponseDto> CreateAsync(CreateAssetCategoryDto input)
    {
       

        try
        {
            logger.LogDebug("Creating asset category");

            if(input.SystemName.IsNullOrWhiteSpace())
            {
                logger.LogInformation("Creating asset category with name: {AssetCategoryName}", input.SystemName);
                throw new Exception("SystemName cannot be Empty!");
            }

            if (input.DisplayName.IsNullOrWhiteSpace())
            {
                throw new Exception("DisplayName cannot be Empty1");
            }

            var assetCategory = new AssetCategory()
            {
                DisplayName = input.DisplayName.Trim(),
                SystemName = input.SystemName.Trim().ToUpper(),
                IsActive = input.IsActive,
                Description = input.Description?.Trim()
            };

            await assetCategoryRepository.InsertAsync(assetCategory);

            var result = new CreateAssetCategoryResponseDto()
            {
                Id = assetCategory.Id
            };

            return result;
        }
        catch(Exception ex)
        {
            throw new Exception("Error creating asset category", ex);
        }
    }


    /// <summary>
    /// Retrieves all asset categories
    /// </summary>
    /// <returns>List of Categories</returns>
    /// <exception cref="Exception"></exception>
    /// 
    public async Task<IEnumerable<GetAssetCategoryDto>> GetListAsync()
    {
        try
        {
            var cacheKey = "AssetCategory_List";

            var cached = await cache.GetAsync(cacheKey);
            if (cached != null)
            {
                // Fix: Use JsonSerializer.Deserialize with the correct overload for byte array input
                return JsonSerializer.Deserialize<List<GetAssetCategoryDto>>(cached, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }

            var result = (await assetCategoryRepository.GetListAsync())
                .Select(x => new GetAssetCategoryDto
                {
                    DisplayName = x.DisplayName,
                    IsActive = x.IsActive,
                    Description = x.Description
                })
                .ToList();

            var serialized = JsonSerializer.SerializeToUtf8Bytes(result);

            await cache.SetAsync(cacheKey, serialized, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting asset categories", ex);
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var assetCategory = await assetCategoryRepository.AnyAsync(d => d.Id==id);

            await cache.RemoveAsync("AssetCategory_List");
            await cache.RemoveAsync($"AssetCategory_{id}");

            if (!assetCategory)
            {
                throw new Exception("Asset category not found");
            }

            await assetCategoryRepository.DeleteAsync(id);

            return true;

        }
        catch (Exception ex)
        {
            throw new Exception("Error deleting asset category", ex);
        }
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateAssetCategoryDto input)
    {
        try
        {
            if (input.SystemName.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("SystemName cannot be null or whitespace", nameof(input.SystemName));
            }

            if (input.DisplayName.IsNullOrWhiteSpace())
            {
                throw new ArgumentException("DisplayName cannot be null or whitespace", nameof(input.DisplayName));
            }

            var assetCategory = await assetCategoryRepository.GetAsync(id);

            if (assetCategory == null)
            {
                throw new InvalidOperationException($"Asset category with ID '{id}' not found.");
            }

            assetCategory.DisplayName = input.DisplayName.Trim();
            assetCategory.SystemName = input.SystemName.Trim().ToUpper();
            assetCategory.IsActive = input.IsActive;
            assetCategory.Description = input.Description?.Trim();

            await assetCategoryRepository.UpdateAsync(assetCategory);

            var updatedDto = new GetAssetCategoryDto
            {
                DisplayName = assetCategory.DisplayName,
                SystemName = assetCategory.SystemName,
                IsActive = assetCategory.IsActive,
                Description = assetCategory.Description
            };

            var serialized = JsonSerializer.SerializeToUtf8Bytes(updatedDto);
            await cache.SetAsync($"AssetCategory_{id}", serialized, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error updating asset category");
            throw;
        }


    }
    public async Task<GetAssetCategoryDto> GetByIdAsync(Guid id)
    {
        var assetCategory = await assetCategoryRepository.FindAsync(id);
        if (assetCategory == null)
        {
            throw new UserFriendlyException("Asset category not found");
        }

        var cacheKey = $"AssetCategory_{id}";
        var cached = await cache.GetAsync(cacheKey);
        if (cached != null)
        {
            return JsonSerializer.Deserialize<GetAssetCategoryDto>(cached, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
        var serialized = JsonSerializer.SerializeToUtf8Bytes(assetCategory, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });
        await cache.SetAsync(cacheKey, serialized, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        });
        // Fix: Use JsonSerializer.Deserialize with the correct overload for byte array input
        // Deserialize the cached data into the GetAssetCategoryDto object
        // var cachedDto = JsonSerializer.Deserialize<GetAssetCategoryDto>(cached, new JsonSerializerOptions

        var result = new GetAssetCategoryDto()
        {
            DisplayName = assetCategory.DisplayName,
            SystemName = assetCategory.SystemName,
            IsActive = assetCategory.IsActive,
            Description = assetCategory.Description
        };

        return result;

    }
}
