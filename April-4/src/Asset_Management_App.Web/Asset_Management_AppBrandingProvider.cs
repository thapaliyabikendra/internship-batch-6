using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;
using Microsoft.Extensions.Localization;
using Asset_Management_App.Localization;

namespace Asset_Management_App.Web;

[Dependency(ReplaceServices = true)]
public class Asset_Management_AppBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<Asset_Management_AppResource> _localizer;

    public Asset_Management_AppBrandingProvider(IStringLocalizer<Asset_Management_AppResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
