using Volo.Abp.Modularity;

namespace Asset_Management_App;

[DependsOn(
    typeof(Asset_Management_AppDomainModule),
    typeof(Asset_Management_AppTestBaseModule)
)]
public class Asset_Management_AppDomainTestModule : AbpModule
{

}
