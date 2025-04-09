using Asset_Management_App.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;
using Volo.Abp.MultiTenancy;

namespace Asset_Management_App.Permissions;

public class Asset_Management_AppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(Asset_Management_AppPermissions.GroupName);

        //Define your own permissions here. Example:
        //myGroup.AddPermission(Asset_Management_AppPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<Asset_Management_AppResource>(name);
    }
}
