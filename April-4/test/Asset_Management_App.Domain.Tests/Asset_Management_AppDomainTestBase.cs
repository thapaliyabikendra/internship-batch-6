using Volo.Abp.Modularity;

namespace Asset_Management_App;

/* Inherit from this class for your domain layer tests. */
public abstract class Asset_Management_AppDomainTestBase<TStartupModule> : Asset_Management_AppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
