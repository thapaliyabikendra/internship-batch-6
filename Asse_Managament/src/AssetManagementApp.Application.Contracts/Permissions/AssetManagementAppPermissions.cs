using AssetManagementApp.Assets;

namespace AssetManagementApp.Permissions;

public static class AssetManagementAppPermissions
{
    public const string GroupName = "AssetManagementApp";

    public static class Assets
    {
        public const string Default = GroupName + ".Assets";
        public const string Create = Default + ".Create";
        public const string Edit = Default + ".Edit";
        public const string Delete = Default + ".Delete";
    }
    
    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
