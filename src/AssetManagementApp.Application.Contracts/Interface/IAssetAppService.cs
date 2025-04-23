using AssetManagementApp.AssetDtos;
using AssetManagementApp.AssetsDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AssetManagementApp.Interface;

public interface IAssetAppService : IApplicationService
{
    public Task<CreateAssetResponseDto> CreateAsync(CreateAssetRequestDto input);
    Task<AssetResponseDto> GetAsync(Guid id);
    Task<List<AssetResponseDto>> GetListAsync();
    Task<bool> DeleteAsync(Guid Id);
    Task<bool> UpdateAsync(Guid id, UpdateAssetDto input);
    

}
