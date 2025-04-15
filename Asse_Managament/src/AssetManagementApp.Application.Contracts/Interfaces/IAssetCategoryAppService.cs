using AssetManagementApp.AssetsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AssetManagementApp.Interfaces;

public interface IAssetCategoryAppService : IApplicationService 
{
    Task<CreateAssetCategoryResponseDto>CreateAsync(CreateAssetCategoryDto input);
    Task<IEnumerable<CreateAssetCategoryDto>> GetAllAssetCategoriesAsync();
    Task<bool> DeleteAssetCategoryAsync(Guid id);

    Task<bool> UpdateAssetCategoryAsync(Guid id, UpdateAssetCategoryDto input);
}
