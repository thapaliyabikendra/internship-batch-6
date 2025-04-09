using AssetManagementApp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace AssetManagementApp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AssetManagementAppController : AbpControllerBase
{
    protected AssetManagementAppController()
    {
        LocalizationResource = typeof(AssetManagementAppResource);
    }
}
