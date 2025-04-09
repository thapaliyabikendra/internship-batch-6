using Volo.Abp.Modularity;

namespace Asset_Management_App;

[DependsOn(
    typeof(Asset_Management_AppApplicationModule),
    typeof(Asset_Management_AppDomainTestModule)
)]
public class Asset_Management_AppApplicationTestModule : AbpModule
{

}
