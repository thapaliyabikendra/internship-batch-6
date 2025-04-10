using AssetManagementApp.Localization;
using Volo.Abp.Application.Services;

namespace AssetManagementApp;

/* Inherit your application services from this class.
 */
public abstract class AssetManagementAppAppService : ApplicationService
{
    protected AssetManagementAppAppService()
    {
        LocalizationResource = typeof(AssetManagementAppResource);
    }
}
