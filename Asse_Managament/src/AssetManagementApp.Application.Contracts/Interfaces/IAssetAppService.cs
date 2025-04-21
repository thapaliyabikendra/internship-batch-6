using AssetManagementApp.Dtos;
using AssetManagementApp.Dtos.AssetDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace AssetManagementApp.Interfaces;

public interface IAssetAppService : IApplicationService
{
    Task<CreateAssetResponseDto> CreateAsync(CreateAssetDto input);
    Task<GetAssetResponseDto> GetByIdAsync(Guid id);
    Task<PagedResultDto<GetAssetResponseDto>>GetListAsync(GetAssetList input);

    Task<bool> UpdateAsync(Guid id, UpdateAssetDto input);

    Task<bool> DeleteAsync(Guid id);

    Task TestRedisCacheAsync();
}
