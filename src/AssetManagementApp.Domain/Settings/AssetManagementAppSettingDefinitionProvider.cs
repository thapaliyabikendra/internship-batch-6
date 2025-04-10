using Volo.Abp.Settings;

namespace AssetManagementApp.Settings;

public class AssetManagementAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(AssetManagementAppSettings.MySetting1));
    }
}
