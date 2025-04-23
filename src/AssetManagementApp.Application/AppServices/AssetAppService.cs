using AssetManagementApp.AssetDtos;
using AssetManagementApp.Dtos;
using AssetManagementApp.Entities;
using AssetManagementApp.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Repositories;

namespace AssetManagementApp.AppServices;

public class AssetAppService(
    IRepository<Asset, Guid> assetRepository,
    IDistributedCache<List<AssetResponseDto>> _cache)
    : ApplicationService, IAssetAppService
{
    private const string CacheKey = "asset-list";

    // Create Logic
    public async Task<CreateAssetResponseDto> CreateAsync(CreateAssetRequestDto input)
    {
        try
        {
            Logger.LogInformation("Creating asset with name: {AssetDisplayName}", input.DisplayName);

            if (input.DisplayName.IsNullOrWhiteSpace())
                throw new UserFriendlyException("DisplayName cannot be null");

            if (input.SystemName.IsNullOrWhiteSpace())
                throw new UserFriendlyException("SystemName cannot be null");

            var asset = new Asset
            {
                DisplayName = input.DisplayName.Trim(),
                SystemName = input.SystemName.Trim().ToUpper(),
                IsActive = input.IsActive,
                Description = input.Description?.Trim(),
                AssetCategoryId = input.AssetCategoryId,
                DepartmentId = input.DepartmentId
            };

            await assetRepository.InsertAsync(asset);

            // Invalidate cache after creation
            await _cache.RemoveAsync(CacheKey);

            return new CreateAssetResponseDto
            {
                Id = asset.Id
            };
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error creating asset");
            throw new UserFriendlyException("Error creating asset", ex.Message);
        }
    }

    // Delete Logic
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var exists = await assetRepository.AnyAsync(a => a.Id == id);
            if (!exists)
                throw new UserFriendlyException("Asset not found.");

            await assetRepository.DeleteAsync(id);

            // Invalidate cache after deletion
            await _cache.RemoveAsync(CacheKey);

            return true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error deleting asset");
            throw new UserFriendlyException("Error deleting asset", ex.Message);
        }
    }

    // Read (Get by ID)
    public async Task<AssetResponseDto> GetAsync(Guid id)
    {
        try
        {
            var asset = await assetRepository.FindAsync(id);
            if (asset == null)
                throw new UserFriendlyException("Asset not found.");

            return new AssetResponseDto
            {
                Id = asset.Id,
                DisplayName = asset.DisplayName,
                SystemName = asset.SystemName,
                IsActive = asset.IsActive,
                Description = asset.Description,
                AssetCategoryId = asset.AssetCategoryId,
                DepartmentId = asset.DepartmentId
            };
        }
        catch (UserFriendlyException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error retrieving asset");
            throw new UserFriendlyException("Error retrieving asset", "500");
        }
    }

    // List (Get All with Cache)
    public async Task<PagedResultDto<AssetResponseDto>> GetListAsync(GetAssetsRequestDto input)
    {
        try
        {
            var cachedData = await _cache.GetAsync(CacheKey);
            List<AssetResponseDto> allAssets;
            if (cachedData != null)
            {
                allAssets = cachedData;
            }
            else
            {
                var query = await assetRepository.GetQueryableAsync();

                allAssets = await query
                    .Where(x => x.IsActive && !x.IsDeleted)
                    .Select(y => new AssetResponseDto
                    {
                        Id = y.Id,
                        DisplayName = y.DisplayName,
                        SystemName = y.SystemName,
                        IsActive = y.IsActive,
                        Description = y.Description,
                        AssetCategoryId = y.AssetCategoryId,
                        DepartmentId = y.DepartmentId
                    })
                    .ToListAsync();

                // Cache the full list
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1)
                };

                await _cache.SetAsync(CacheKey, allAssets, cacheOptions);
            }

            // Apply pagination on cached or fresh data
            var totalCount = allAssets.Count;

            var pagedAssets = allAssets
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            return new PagedResultDto<AssetResponseDto>(
                totalCount,
                pagedAssets
            );
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error retrieving asset list");
            throw new UserFriendlyException("Error retrieving asset list", "500");
        }
    }
    // Update Logic
    public async Task<bool> UpdateAsync(Guid id, UpdateAssetDto input)
    {
        try
        {
            var asset = await assetRepository.GetAsync(id);
            if (asset == null)
                throw new UserFriendlyException("Asset not found.");

            ValidateInput(input);

            asset.DisplayName = input.DisplayName.Trim();
            asset.SystemName = input.SystemName.Trim().ToUpper();
            asset.IsActive = input.IsActive;
            asset.Description = input.Description?.Trim();
            asset.AssetCategoryId = input.AssetCategoryId;
            asset.DepartmentId = input.DepartmentId;

            await assetRepository.UpdateAsync(asset);

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
            Logger.LogError(ex, "Error updating asset");
            throw new UserFriendlyException("Error updating asset", ex.Message);
        }
    }

    private void ValidateInput(UpdateAssetDto input)
    {
        if (input.SystemName.IsNullOrWhiteSpace())
            throw new UserFriendlyException("SystemName cannot be null");

        if (input.DisplayName.IsNullOrWhiteSpace())
            throw new UserFriendlyException("DisplayName cannot be null");
    }
}
