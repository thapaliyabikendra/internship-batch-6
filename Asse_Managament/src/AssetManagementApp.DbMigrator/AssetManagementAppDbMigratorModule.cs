using AssetManagementApp.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace AssetManagementApp.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AssetManagementAppEntityFrameworkCoreModule),
    typeof(AssetManagementAppApplicationContractsModule)
)]
public class AssetManagementAppDbMigratorModule : AbpModule
{
}
