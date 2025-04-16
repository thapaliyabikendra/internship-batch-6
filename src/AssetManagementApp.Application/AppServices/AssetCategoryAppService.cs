using AssetManagementApp.Assets;
using AssetManagementApp.AssetsDtos;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;

namespace AssetManagementApp.AppServices;

public class AssetCategoryAppService
    (IRepository<AssetCategory, Guid> assetCategoryRepository)
    : ApplicationService, IAssetCategoryAppService
{
    public async Task<CreateAssetCategoryResponseDto> CreateAsync(CreateAssetCategoryRequestDto input)
    {
        try
        {
            Logger.LogInformation("Creating asset category with name: {AssetCategoryName}", input.DisplayName);

            if (input.DisplayName.IsNullOrWhiteSpace())
            {
                throw new Exception("DisplayName cannot be null");
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
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error creating asset category");
            throw new Exception("Error creating asset category", ex);
        }
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        try
        {
            var assetCategory = await assetCategoryRepository.AnyAsync(a => a.Id == id);
            if (!assetCategory)
            {
                throw new Exception("Asset Category not found.");
            }

            await assetCategoryRepository.DeleteAsync(id);
            return true;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error retrieving or deleting asset category");
            throw new Exception("Error Reterving Asset Catergory.", ex);
        }
    }

    public async Task<AssetCategoryResponseDto> GetAsync(Guid id)
    {
        try
        {
            var assetCategory = await assetCategoryRepository.FindAsync(id);
            if (assetCategory is null)
            {
                throw new UserFriendlyException("Asset category not found.");
            }

            var result = new AssetCategoryResponseDto()
            {
                Id = assetCategory.Id,
                DisplayName = assetCategory.DisplayName,
                IsActive = assetCategory.IsActive,
                Description = assetCategory.Description
            };
            return result;
        }
        catch (UserFriendlyException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error retrieving asset category");
            throw new UserFriendlyException("Error Reterving Asset Catergory.", "500");
        }
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateAssetCatrgoryDto input)
    {
        try
        {
            var assetCategory = await assetCategoryRepository.GetAsync(id);
            if (assetCategory == null)
            {
                throw new UserFriendlyException("Asset Category not found.");
            }

            if (input.SystemName.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("SystemName cannot be null");
            }
            if (input.DisplayName.IsNullOrWhiteSpace())
            {
                throw new UserFriendlyException("DisplayName cannot be null");
            }

            assetCategory.DisplayName = input.DisplayName.Trim();
            assetCategory.SystemName = input.SystemName.Trim().ToUpper();
            assetCategory.IsActive = input.IsActive;
            assetCategory.Description = input.Description?.Trim();

            await assetCategoryRepository.UpdateAsync(assetCategory);

            return true;
        }
        catch (UserFriendlyException)
        {
            throw;
        }
        catch (Exception ex)
        {
            Logger.LogError(ex, "Error updating asset category");
            throw new UserFriendlyException("Asset Category not found.");
        }
    }
}
