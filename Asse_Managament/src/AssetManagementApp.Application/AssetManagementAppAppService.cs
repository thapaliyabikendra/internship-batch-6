using AssetManagementApp.Localization;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
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
