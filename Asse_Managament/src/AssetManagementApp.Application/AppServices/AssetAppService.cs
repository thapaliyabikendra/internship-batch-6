using AssetManagementApp.Assets;
using AssetManagementApp.Dtos.AssetDtos;
using AssetManagementApp.Interfaces;
using AssetManagementApp.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities.Caching;
using Volo.Abp.Domain.Repositories;

namespace AssetManagementApp.AppServices;
[Authorize(AssetManagementAppPermissions.Assets.Default)]
public class AssetAppService(ILogger<AssetAppService> logger, IRepository<Asset,Guid> assetRepository)
    : ApplicationService, IAssetAppService
{
    public async Task<CreateAssetResponseDto> CreateAsync(CreateAssetDto input)
    {
        try
        {
            logger.LogDebug("Starting Asset App Service");
            // validate inputs
            if (input.AssetName.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("AssetName cannot be Empty!");
            }
           if(input.SerialNumber.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("SerialNumber cannot be Empty!");
            }

            //create objects for model and remove extr spaces...
            var asset = new Asset()
            {
                AssetName = input.AssetName.Trim(),
                SerialNumber = input.SerialNumber.Trim(),
                AssetCategoryId = input.AssetCategoryId,
                DepartmentId = input.DepartmentId,
                ReceivedDate = input.ReceivedDate
            };

            // now add the above objects into repository or database
            await assetRepository.InsertAsync(asset);

            // now check the id and return the above result,
            var result = new CreateAssetResponseDto()
            { 
                Id = asset.Id
            };

            return result;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Error creating asset");
            throw new UserFriendlyException("An error occurred while creating the asset.");
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

    public Task<GetAssetResponseDto> GetListAsync()
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAsync(Guid id, UpdateAssetDto input)
    {
        throw new NotImplementedException();
    }
}
