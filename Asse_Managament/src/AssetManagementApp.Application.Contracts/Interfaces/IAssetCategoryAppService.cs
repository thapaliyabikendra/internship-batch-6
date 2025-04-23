using AssetManagementApp.Dtos.AssetsCategoryDtos;
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
    Task<IEnumerable<GetAssetCategoryDto>> GetListAsync();
    Task<GetAssetCategoryDto> GetByIdAsync(Guid id);
    Task<bool> DeleteAsync(Guid id);

    Task<bool> UpdateAsync(Guid id, UpdateAssetCategoryDto input);
}
