using AssetManagementApp.AssetDtos;
using AssetManagementApp.AssetsDtos;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AssetManagementApp.Interfaces;

public interface IAssetAppService : IApplicationService
{
    Task<CreateAssetResponseDto> CreateAsync(CreateAssetDto input);
    Task<IQueryable<CreateAssetDto>> GetAssetByIdAsync(Guid id);

    Task<bool> UpdateAssetAsync(Guid id, UpdateAssetDto input);

    Task<bool> DeleteAssetAsync(Guid id);
}
