using Microsoft.Extensions.Localization;
using AssetManagementApp.Localization;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace AssetManagementApp;

[Dependency(ReplaceServices = true)]
public class AssetManagementAppBrandingProvider : DefaultBrandingProvider
{
    private IStringLocalizer<AssetManagementAppResource> _localizer;

    public AssetManagementAppBrandingProvider(IStringLocalizer<AssetManagementAppResource> localizer)
    {
        _localizer = localizer;
    }

    public override string AppName => _localizer["AppName"];
}
