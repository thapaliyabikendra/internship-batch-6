using Volo.Abp.Modularity;

namespace Asset_Management_App;

public abstract class Asset_Management_AppApplicationTestBase<TStartupModule> : Asset_Management_AppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
