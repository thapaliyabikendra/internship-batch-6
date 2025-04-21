using Volo.Abp.PermissionManagement;
using Volo.Abp.SettingManagement;
using Volo.Abp.Account;
using Volo.Abp.Identity;
using Volo.Abp.AutoMapper;
using Volo.Abp.FeatureManagement;
using Volo.Abp.Modularity;
using Volo.Abp.TenantManagement;
using Volo.Abp.Caching;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.BlobStoring;

namespace AssetManagementApp;

[DependsOn(
    typeof(AssetManagementAppDomainModule),
    typeof(AssetManagementAppApplicationContractsModule),
    typeof(AbpPermissionManagementApplicationModule),
    typeof(AbpFeatureManagementApplicationModule),
    typeof(AbpIdentityApplicationModule),
    typeof(AbpAccountApplicationModule),
    typeof(AbpTenantManagementApplicationModule),
    typeof(AbpSettingManagementApplicationModule)
    )]
public class AssetManagementAppApplicationModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        Configure<AbpAutoMapperOptions>(options =>
        {
            options.AddMaps<AssetManagementAppApplicationModule>();
        });

        Configure<AbpDistributedCacheOptions>(options =>
        {
            options.KeyPrefix = "AssetManagement";
        });

        var configuration = context.Services.GetConfiguration();

        context.Services.Configure<MySettings>(
            configuration.GetSection("MySettings"));


        //Configure<AbpBlobStoringOptions>(options =>
        //{
        //    options.Containers.Configure("StorageContainer", container =>
        //    {
        //        container.UseFileSystem(fileSystem =>
        //        {
        //            fileSystem.BasePath = "blobs"; // relative to the app root
        //        });
        //    });
        //});
    }
}
