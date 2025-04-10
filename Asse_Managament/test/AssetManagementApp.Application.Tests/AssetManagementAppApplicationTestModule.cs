using Volo.Abp.Modularity;

namespace AssetManagementApp;

[DependsOn(
    typeof(AssetManagementAppApplicationModule),
    typeof(AssetManagementAppDomainTestModule)
)]
public class AssetManagementAppApplicationTestModule : AbpModule
{

}
