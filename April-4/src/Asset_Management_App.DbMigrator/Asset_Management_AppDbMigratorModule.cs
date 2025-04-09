using Asset_Management_App.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Asset_Management_App.DbMigrator;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(Asset_Management_AppEntityFrameworkCoreModule),
    typeof(Asset_Management_AppApplicationContractsModule)
)]
public class Asset_Management_AppDbMigratorModule : AbpModule
{
}
