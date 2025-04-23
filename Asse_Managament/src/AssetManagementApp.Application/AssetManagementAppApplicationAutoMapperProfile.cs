using AssetManagementApp.Assets;
using AssetManagementApp.Dtos.AssetDtos;
using AssetManagementApp.Dtos.AssetsCategoryDtos;
using AssetManagementApp.Dtos.DepartmentDtos;
using AutoMapper;
using Volo.Abp.Account;
using Volo.Abp.Caching;

namespace AssetManagementApp;

public class AssetManagementAppApplicationAutoMapperProfile : Profile
{
    public AssetManagementAppApplicationAutoMapperProfile()
    {
        CreateMap<CreateAssetCategoryDto, AssetCategory>();

        CreateMap<AssetCategory, GetAssetCategoryDto>();

        CreateMap<Department, CreateDepartmentDto>();

        CreateMap<Asset, CreateAssetDto>();
    }
}
