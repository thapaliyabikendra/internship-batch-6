using Asset_Management_App.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Asset_Management_App.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class Asset_Management_AppController : AbpControllerBase
{
    protected Asset_Management_AppController()
    {
        LocalizationResource = typeof(Asset_Management_AppResource);
    }
}
