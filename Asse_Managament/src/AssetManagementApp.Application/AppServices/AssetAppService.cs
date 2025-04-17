using AssetManagementApp.AssetDtos;
using AssetManagementApp.Assets;
using AssetManagementApp.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace AssetManagementApp.AppServices;

public class AssetAppService(ILogger<AssetAppService> logger, IRepository<Asset,Guid> assetRepository)
    : ApplicationService, IAssetAppService
{
    public Task<CreateAssetResponseDto> CreateAsync(CreateAssetDto input)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAssetAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<IQueryable<CreateAssetDto>> GetAssetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateAssetAsync(Guid id, UpdateAssetDto input)
    {
        throw new NotImplementedException();
    }
}
