using AssetManagementApp.Assets;
using AssetManagementApp.AssetsDtos;
using AssetManagementApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AssetManagementApp.AppServices;

public class AssetCategoryAppService(
    IRepository<AssetCategory, Guid> assetCategoryRepository)
    : ApplicationService, IAssetCategoryAppService
{
    public async Task<CreateAssetCategoryResponseDto> CreateAsync(CreateAssetCategoryDto input)
    {
        try
        {
            if(input.SystemName.IsNullOrWhiteSpace())
            {
                throw new Exception("SystemName cannot be null");
            }
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
        catch(Exception ex)
        {
            throw new Exception("Error creating asset category", ex);
        }
    }

    // retrives all asset categories
    public async Task<IEnumerable<CreateAssetCategoryDto>> GetAllAssetCategoriesAsync()
    {
        try
        {
            var assetCategories = await assetCategoryRepository.GetListAsync();

            var result = assetCategories.Select(x => new CreateAssetCategoryDto
            {
                DisplayName = x.DisplayName,
                SystemName = x.SystemName,
                IsActive = x.IsActive,
                Description = x.Description
            });

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception("Error getting asset categories", ex);
        }
    }


}

