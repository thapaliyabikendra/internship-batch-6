using Volo.Abp.Modularity;

namespace AssetManagementApp;

public abstract class AssetManagementAppApplicationTestBase<TStartupModule> : AssetManagementAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
