using AssetManagementApp.AssetDtos;
using AssetManagementApp.AssetsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AssetManagementApp;

public interface IAssetCategoryAppService : IApplicationService
{
    Task<CreateAssetCategoryResponseDto> CreateAsync(CreateAssetCategoryRequestDto input);
    Task<AssetCategoryResponseDto> GetAsync(Guid Id);
    Task<List<AssetCategoryResponseDto>> GetListAsync();

    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(Guid id, UpdateAssetCatrgoryDto input);
}
