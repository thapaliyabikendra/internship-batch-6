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
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Caching;
using Volo.Abp.Domain.Entities.Caching;
using Volo.Abp.Domain.Repositories;

namespace AssetManagementApp.AppServices;
[Authorize(AssetManagementAppPermissions.Assets.Default)]
public class AssetAppService(ILogger<AssetAppService> logger, IRepository<Asset,Guid> assetRepository, IDistributedCache<CreateAssetDto> cache)
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
        var cachKey = $"Asset_{id}";
        var cachedAsset = await cache.GetAsync(cachKey);

        if (cachedAsset != null)
        {
            await cache.RemoveAsync(cachKey);
        }

        // Check if the asset exists
        var assetExists = await assetRepository.AnyAsync(x => x.Id == id);
        if (!assetExists)
        {
            throw new UserFriendlyException("Asset not found");
        }

        var assets = await assetRepository.FindAsync(id);
        var assetDto = ObjectMapper.Map<Asset, CreateAssetDto>(assets);

        // cache for some period of time
        await cache.SetAsync(cachKey, assetDto, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
        });
        var asset = await assetRepository.FindAsync(id);
        if(asset == null)
        {
            throw new UserFriendlyException("Id not defined");
        }

        var result = new GetAssetResponseDto()
        {
            AssetName = asset.AssetName,
            SerialNumber = asset.SerialNumber,
            AssetCategoryId = asset.AssetCategoryId,
            DepartmentId = asset.DepartmentId,
            ReceivedDate = asset.ReceivedDate
        };

        return result;
    }

    public async Task<PagedResultDto<GetAssetResponseDto>> GetListAsync(GetAssetList input)
    {
        try
        {
            logger.LogDebug("Starting Asset App Service");
            if (input.MaxResultCount <= 0)
            {
                throw new UserFriendlyException("MaxResultCount cannot be less than or equal to 0");
            }
            if (input.SkipCount < 0)
            {
                throw new UserFriendlyException("SkipCount cannot be less than 0");
            }

            var asset = await assetRepository.GetListAsync();
            if (!string.IsNullOrEmpty(input.Filter))
            {
                asset = asset.Where(x => x.AssetName.Contains(input.Filter) || x.SerialNumber.Contains(input.Filter))
                    .ToList();
            }

            // now check the total count of the asset
            var totalCount = asset.Count();

            var items = asset.Skip(input.SkipCount).Take(input.MaxResultCount).ToList();
            var result = new List<GetAssetResponseDto>();
            foreach (var item in items)
            {
                result.Add(new GetAssetResponseDto()
                {
                    AssetName = item.AssetName,
                    SerialNumber = item.SerialNumber,
                    AssetCategoryId = item.AssetCategoryId,
                    DepartmentId = item.DepartmentId,
                    ReceivedDate = item.ReceivedDate
                });
            }
            return new PagedResultDto<GetAssetResponseDto>(totalCount, result);
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
}
