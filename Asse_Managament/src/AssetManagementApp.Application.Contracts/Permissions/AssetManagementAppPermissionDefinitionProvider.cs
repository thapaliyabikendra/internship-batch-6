using AssetManagementApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace AssetManagementApp.Permissions;

public class AssetManagementAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AssetManagementAppPermissions.GroupName);

        var assetsPermission = myGroup.AddPermission(AssetManagementAppPermissions.Assets.Default, L("Permission:Assets"));
        assetsPermission.AddChild(AssetManagementAppPermissions.Assets.Create, L("Permission:Create"));
        assetsPermission.AddChild(AssetManagementAppPermissions.Assets.Edit, L("Permissiom:Edit"));
        assetsPermission.AddChild(AssetManagementAppPermissions.Assets.Delete, L("Permission:Delete"));

        //Define your own permissions here. Example:
        //myGroup.AddPermission(AssetManagementAppPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AssetManagementAppResource>(name);
    }
}
