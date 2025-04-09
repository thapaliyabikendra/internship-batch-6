using Asset_Management_App.Localization;
using Volo.Abp.Application.Services;

namespace Asset_Management_App;

/* Inherit your application services from this class.
 */
public abstract class Asset_Management_AppAppService : ApplicationService
{
    protected Asset_Management_AppAppService()
    {
        LocalizationResource = typeof(Asset_Management_AppResource);
    }
}
