using AssetManagementApp.AssetDtos;
using AssetManagementApp.Assets;
using AssetManagementApp.AssetsDtos;
using AssetManagementApp.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace AssetManagementApp.AppServices
{
    public class AssetCategoryAppService(
        IRepository<AssetCategory, Guid> assetCategoryRepository,
        IDistributedCache<List<AssetCategoryResponseDto>> _cache)
        : ApplicationService, IAssetCategoryAppService
    {
        private const string CacheKey = "asset-category-list";

        // Create Logic
        public async Task<CreateAssetCategoryResponseDto> CreateAsync(CreateAssetCategoryRequestDto input)
        {
            try
            {
                Logger.LogInformation("Creating asset category with name: {AssetCategoryName}", input.DisplayName);

                if (input.DisplayName.IsNullOrWhiteSpace())
                {
                    throw new UserFriendlyException("DisplayName cannot be null");
                }

                var assetCategory = new AssetCategory
                {
                    DisplayName = input.DisplayName.Trim(),
                    SystemName = input.SystemName.Trim().ToUpper(),
                    IsActive = input.IsActive,
                    Description = input.Description?.Trim()
                };

                await assetCategoryRepository.InsertAsync(assetCategory);

                // Invalidate cache after creation
                await _cache.RemoveAsync(CacheKey);

                return new CreateAssetCategoryResponseDto
                {
                    Id = assetCategory.Id
                };
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error creating asset category");
                throw new UserFriendlyException("Error creating asset category", ex.Message);
            }
        }

        // Delete Logic
        public async Task<bool> DeleteAsync(Guid id)
        {
            try
            {
                var exists = await assetCategoryRepository.AnyAsync(a => a.Id == id);
                if (!exists)
                {
                    throw new UserFriendlyException("Asset category not found.");
                }

                await assetCategoryRepository.DeleteAsync(id);

                // Invalidate cache after deletion
                await _cache.RemoveAsync(CacheKey);

                return true;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error deleting asset category");
                throw new UserFriendlyException("Error deleting asset category", ex.Message);
            }
        }

        // Get by ID
        public async Task<AssetCategoryResponseDto> GetAsync(Guid id)
        {
            try
            {
                var assetCategory = await assetCategoryRepository.FindAsync(id);
                if (assetCategory is null)
                {
                    throw new UserFriendlyException("Asset category not found.");
                }

                return new AssetCategoryResponseDto
                {
                    Id = assetCategory.Id,
                    DisplayName = assetCategory.DisplayName,
                    IsActive = assetCategory.IsActive,
                    Description = assetCategory.Description
                };
            }
            catch (UserFriendlyException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error retrieving asset category");
                throw new UserFriendlyException("Error retrieving asset category", "500");
            }
        }

        // Get List Logic with Cache
        public async Task<List<AssetCategoryResponseDto>> GetListAsync()
        {
            try
            {
                var cachedData = await _cache.GetAsync(CacheKey);
                if (cachedData != null)
                {
                    return cachedData;
                }

                var assetCategories = await (await assetCategoryRepository.GetQueryableAsync())
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .Select(y => new AssetCategoryResponseDto
                    {
                        Id = y.Id,
                        DisplayName = y.DisplayName,
                        IsActive = y.IsActive,
                        Description = y.Description
                    })
                    .ToListAsync();

                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                };

                await _cache.SetAsync(CacheKey, assetCategories, cacheOptions);

                return assetCategories;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error retrieving asset categories");
                throw new UserFriendlyException("Error retrieving asset categories", "500");
            }
        }

        // Update Logic
        public async Task<bool> UpdateAsync(Guid id, UpdateAssetCatrgoryDto input)
        {
            try
            {
                var assetCategory = await assetCategoryRepository.GetAsync(id);
                if (assetCategory == null)
                {
                    throw new UserFriendlyException("Asset category not found.");
                }

                ValidateInput(input);

                assetCategory.DisplayName = input.DisplayName.Trim();
                assetCategory.SystemName = input.SystemName.Trim().ToUpper();
                assetCategory.IsActive = input.IsActive;
                assetCategory.Description = input.Description?.Trim();

                await assetCategoryRepository.UpdateAsync(assetCategory);

                // Invalidate cache after update
                await _cache.RemoveAsync(CacheKey);

                return true;
            }
            catch (UserFriendlyException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error updating asset category");
                throw new UserFriendlyException("Error updating asset category");
            }
        }

        private void ValidateInput(UpdateAssetCatrgoryDto input)
        {
            if (input.SystemName.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("SystemName cannot be null");
            }

            if (input.DisplayName.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("DisplayName cannot be null");
            }
        }
    }
}
