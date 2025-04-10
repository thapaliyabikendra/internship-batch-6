using Volo.Abp.Modularity;

namespace AssetManagementApp;

/* Inherit from this class for your domain layer tests. */
public abstract class AssetManagementAppDomainTestBase<TStartupModule> : AssetManagementAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
