using Volo.Abp.Modularity;

namespace AssetManagementApp;

[DependsOn(
    typeof(AssetManagementAppDomainModule),
    typeof(AssetManagementAppTestBaseModule)
)]
public class AssetManagementAppDomainTestModule : AbpModule
{

}
