using AssetManagementApp.AssetDtos;
using AssetManagementApp.Entities;
using AssetManagementApp.Interface;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AssetManagementApp.AppServices;

public class AssetAppService(
    IRepository<Asset, Guid> assetRepository)
    : ApplicationService, IAssetAppService
{
    //Create Logic
    public async Task<CreateAssetResponseDto> CreateAsync(CreateAssetRequestDto input)
    {
        try
        {
            Logger.LogInformation("Creating asset with name: {AssetDisplayName}", input.DisplayName);

            if (input.DisplayName.IsNullOrWhiteSpace())
            {
                throw new Exception("DisplayName cannot be null");
            }

            if (input.SystemName.IsNullOrWhiteSpace())
            {
                throw new Exception("SystemName cannot be null");
            }

            var asset = new Asset
            {
                DisplayName = input.DisplayName.Trim(),
                SystemName = input.SystemName.Trim().ToUpper(),
                IsActive = input.IsActive,
                Description = input.Description?.Trim(),
                AssetCategoryId = input.AssetCategoryId,
                DepartmentId = input.DepartmentId
            };

            await assetRepository.InsertAsync(asset, autoSave: true);

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

    //Delete Logic
    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var department = await assetRepository.AnyAsync(a => a.Id == id);
            if (!department)
            {
                throw new Exception("Asset Category not found.");
            }

            await assetRepository.DeleteAsync(id);
            return true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error retrieving or deleting department");
            throw new Exception("Error Reterving department", ex);
        }
    }


    //Read Logic
    public async Task<AssetResponseDto> GetAsync(Guid id)
    {
        try
        {
            var asset = await assetRepository.FindAsync(id);
            if (asset == null)
            {
                throw new UserFriendlyException("Asset not found.");
            }

            var result = new AssetResponseDto
            {
                Id = asset.Id,
                DisplayName = asset.DisplayName,
                SystemName = asset.SystemName,
                IsActive = asset.IsActive,
                Description = asset.Description,
                AssetCategoryId = asset.AssetCategoryId,
                DepartmentId = asset.DepartmentId
            };

            return result;
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
    //Update Logic

    public async Task<bool> UpdateAsync(Guid id, UpdateAssetDto input)
    {
        try
        {
            var asset = await assetRepository.GetAsync(id);
            if (asset == null)
            {
                throw new UserFriendlyException("Asset not found.");
            }

            ValidateInput(input);

            asset.DisplayName = input.DisplayName.Trim();
            asset.SystemName = input.SystemName.Trim().ToUpper();
            asset.IsActive = input.IsActive;
            asset.Description = input.Description?.Trim();
            asset.AssetCategoryId = input.AssetCategoryId;
            asset.DepartmentId = input.DepartmentId;

            await assetRepository.UpdateAsync(asset);

            return true;
        }
        catch (UserFriendlyException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error updating asset");
            throw new UserFriendlyException("Error updating asset");
        }
    }

    private void ValidateInput(UpdateAssetDto input)
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
