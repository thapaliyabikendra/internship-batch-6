using AssetManagementApp.Assets;
using AssetManagementApp.Dtos.AssetDtos;
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
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities.Caching;
using Volo.Abp.Domain.Repositories;

namespace AssetManagementApp.AppServices;
[Authorize(AssetManagementAppPermissions.Assets.Default)]
public class AssetAppService(ILogger<AssetAppService> logger, IRepository<Asset,Guid> assetRepository, IDistributedCache cache)
    : ApplicationService, IAssetAppService
{
    public async Task<CreateAssetResponseDto> CreateAsync(CreateAssetDto input)
    {
        try
        {
            Logger.LogInformation("Creating asset with name: {AssetName}", input.AssetName);

            if (input.AssetName.IsNullOrWhiteSpace())
            {
                throw new Exception("AssetName cannot be empty");
            }

            if (input.SerialNumber.IsNullOrWhiteSpace())
            {
                throw new Exception("SerialNumber cannot be empty");
            }

            var asset = new Asset
            {
                AssetName = input.AssetName.Trim(),
                SerialNumber = input.SerialNumber.Trim().ToUpper(),
                AssetCategoryId = input.AssetCategoryId,
                DepartmentId = input.DepartmentId,
                ReceivedDate = input.ReceivedDate
            };

            //var categoryExists = await assetRepository.AnyAsync(x => x.Id == input.AssetCategoryId);
            //if (!categoryExists)
            //    throw new UserFriendlyException("Asset Category not found");

            //var departmentExists = await assetRepository.AnyAsync(x => x.Id == input.DepartmentId);
            //if (!departmentExists)
            //    throw new UserFriendlyException("Department not found");

            await assetRepository.InsertAsync(asset);

            var result = new CreateAssetResponseDto
            {
                Id = asset.Id
            };

            return result;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error creating asset");
            throw new Exception("Error creating asset", ex);
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            logger.LogDebug("Starting Asset App Service");
            var asset = assetRepository.AnyAsync(x => x.Id == id);
            if (asset == null)
            {
                throw new UserFriendlyException("Asset not found");
            }

            await assetRepository.DeleteAsync(id);

            var cacheKey = $"Asset_{id}";
            await cache.RemoveAsync(cacheKey);

            logger.LogDebug("Asset deleted and cache invalidated.");

            return true;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error deleting asset");
            throw new UserFriendlyException("An error occurred while deleting the asset.");
        }
    }
    public async Task<GetAssetResponseDto> GetByIdAsync(Guid id)
    {
        try
        {
            var cacheKey = $"Asset_{id}";

            // Try to get cached byte data
            var cachedData = await cache.GetAsync(cacheKey);
            if (cachedData != null)
            {
                // Deserialize from byte[] to GetAssetResponseDto
                var cachedAsset = JsonSerializer.Deserialize<GetAssetResponseDto>(cachedData);
                return cachedAsset;
            }

            // Check if asset exists
            var assetExists = await assetRepository.AnyAsync(x => x.Id == id);
            if (!assetExists)
            {
                throw new UserFriendlyException("Asset not found");
            }

            // Get the asset from DB
            var asset = await assetRepository.FindAsync(id);
            var assetDto = ObjectMapper.Map<Asset, GetAssetResponseDto>(asset);

            // Cache the result
            var serializedData = JsonSerializer.SerializeToUtf8Bytes(assetDto);
            await cache.SetAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
            });

            // Return the asset DTO
            return assetDto;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting asset by ID");
            throw new UserFriendlyException("An error occurred while getting the asset.");
        }
        //var cacheKey = $"Asset_{id}";
        //var cachedData = await cache.GetAsync(cacheKey);

        //if (cachedData != null)
        //{
        //    var cachedAsset = JsonSerializer.Deserialize<GetAssetResponseDto>(cachedData);

        //    var cachedResponse = new GetAssetResponseDto
        //    {
        //        AssetName = cachedAsset.AssetName,
        //        SerialNumber = cachedAsset.SerialNumber,
        //        AssetCategoryId = cachedAsset.AssetCategoryId,
        //        DepartmentId = cachedAsset.DepartmentId,
        //        ReceivedDate = cachedAsset.ReceivedDate
        //    };

        //    return cachedResponse;
        //}


        //var assets = await assetRepository.FindAsync(id);
        //var assetDto = ObjectMapper.Map<Asset, GetAssetResponseDto>(assets);

        //// Save to cache for next time
        //var serializedData = JsonSerializer.SerializeToUtf8Bytes(assetDto);
        //await cache.SetAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
        //{
        //    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10)
        //});


        //var asset = await assetRepository.FindAsync(id);
        //if (asset == null)
        //{
        //    throw new UserFriendlyException("Id not defined");
        //}

        //var result = new GetAssetResponseDto()
        //{
        //    AssetName = asset.AssetName,
        //    SerialNumber = asset.SerialNumber,
        //    AssetCategoryId = asset.AssetCategoryId,
        //    DepartmentId = asset.DepartmentId,
        //    ReceivedDate = asset.ReceivedDate
        //};

        //return result;
    }

    //public async Task<PagedResultDto<GetAssetResponseDto>> GetListAsync(GetAssetList input)
    //{
    //    try
    //    {
    //        logger.LogDebug("Starting Asset App Service");
    //        if (input.MaxResultCount <= 0)
    //        {
    //            throw new UserFriendlyException("MaxResultCount cannot be less than or equal to 0");
    //        }
    //        if (input.SkipCount < 0)
    //        {
    //            throw new UserFriendlyException("SkipCount cannot be less than 0");
    //        }

    //        var asset = await assetRepository.GetListAsync();
    //        if (!string.IsNullOrEmpty(input.Filter))
    //        {
    //            asset = asset.Where(x => x.AssetName.ToUpper().Contains(input.Filter) || x.SerialNumber.ToUpper().Contains(input.Filter))
    //                .ToList();
    //        }

    //        // now check the total count of the asset
    //        var totalCount = asset.Count();

    //        var items = asset.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
    //        var result = new List<GetAssetResponseDto>();
    //        foreach (var item in items)
    //        {
    //            result.Add(new GetAssetResponseDto()
    //            {
    //                AssetName = item.AssetName,
    //                SerialNumber = item.SerialNumber,
    //                AssetCategoryId = item.AssetCategoryId,
    //                DepartmentId = item.DepartmentId,
    //                ReceivedDate = item.ReceivedDate
    //            });
    //        }
    //        return new PagedResultDto<GetAssetResponseDto>(totalCount, result);
    //    }
    //    catch (Exception ex)
    //    {
    //        logger.LogError(ex, "Error getting asset list");
    //        throw new UserFriendlyException("An error occurred while getting the asset list.");
    //    }  
    //}
    public async Task<PagedResultDto<GetAssetResponseDto>> GetListAsync(GetAssetList input)
    {
        try
        {
            logger.LogDebug("Starting Asset App Service");

            if (input.MaxResultCount <= 0)
                throw new UserFriendlyException("MaxResultCount cannot be less than or equal to 0");

            if (input.SkipCount < 0)
                throw new UserFriendlyException("SkipCount cannot be less than 0");

            // Generate unique cache key based on paging + filter
            var filterKey = input.Filter?.ToUpper() ?? "NO_FILTER";
            var cacheKey = $"AssetList_{filterKey}_{input.SkipCount}_{input.MaxResultCount}";

            // Try to get from cache
            var cachedData = await cache.GetAsync(cacheKey);
            if (cachedData != null)
            {
                var cachedList = JsonSerializer.Deserialize<PagedResultDto<GetAssetResponseDto>>(cachedData);
                return cachedList;
            }

            // Query all
            var asset = await assetRepository.GetListAsync();

            if (!string.IsNullOrWhiteSpace(input.Filter))
            {
                asset = asset.Where(x =>
                    x.AssetName.ToUpper().Contains(input.Filter.ToUpper()) ||
                    x.SerialNumber.ToUpper().Contains(input.Filter.ToUpper())
                ).ToList();
            }

            var totalCount = asset.Count();
            var items = asset
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount)
                .ToList();

            var result = items.Select(item => new GetAssetResponseDto
            {
                AssetName = item.AssetName,
                SerialNumber = item.SerialNumber,
                AssetCategoryId = item.AssetCategoryId,
                DepartmentId = item.DepartmentId,
                ReceivedDate = item.ReceivedDate
            }).ToList();

            var response = new PagedResultDto<GetAssetResponseDto>(totalCount, result);

            // Cache the result
            var serializedData = JsonSerializer.SerializeToUtf8Bytes(response);
            await cache.SetAsync(cacheKey, serializedData, new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });

            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error getting asset list");
            throw new UserFriendlyException("An error occurred while getting the asset list.");
        }
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateAssetDto input)
    {
        var asset = await assetRepository.FindAsync(id);
        if (asset == null)
        {
            throw new UserFriendlyException("Id not defined");
        }

        if (input.AssetName.IsNullOrWhiteSpace())
        {
            throw new UserFriendlyException("AssetName cannot be Empty!");
        }
        if (input.SerialNumber.IsNullOrWhiteSpace())
        {
            throw new UserFriendlyException("SerialNumber cannot be Empty!");
        }

        asset.AssetName = input.AssetName.Trim();
        asset.SerialNumber = input.SerialNumber.Trim();
        asset.AssetCategoryId = input.AssetCategoryId;
        asset.DepartmentId = input.DepartmentId;
        asset.ReceivedDate = input.ReceivedDate;

        await assetRepository.UpdateAsync(asset);

        return true;
    }

    // testing redis api
    public async Task TestRedisCacheAsync()
    {
        await cache.SetStringAsync("test-key", "hello redis");

        var value = await cache.GetStringAsync("test-key");

        Console.WriteLine($"Redis returned: {value}"); // Optional logging
    }
}
