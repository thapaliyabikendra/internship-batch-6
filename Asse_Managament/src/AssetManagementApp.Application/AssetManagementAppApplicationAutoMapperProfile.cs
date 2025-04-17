using AssetManagementApp.AssetDtos;
using AssetManagementApp.Assets;
using AssetManagementApp.AssetsDtos;
using AssetManagementApp.DepartmentDtos;
using AutoMapper;
using Volo.Abp.Account;
using Volo.Abp.Caching;

namespace AssetManagementApp;

public class AssetManagementAppApplicationAutoMapperProfile : Profile
{
    public AssetManagementAppApplicationAutoMapperProfile()
    {
        CreateMap<AssetCategory, CreateAssetCategoryDto>();

        CreateMap<Department, CreateDepartmentDto>();

        CreateMap<Asset, CreateAssetDto>();
    }
}
