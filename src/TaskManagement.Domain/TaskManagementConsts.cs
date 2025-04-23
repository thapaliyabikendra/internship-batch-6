using Volo.Abp.Identity;

namespace TaskManagement;

public static class TaskManagementConsts
{
    public const string DbTablePrefix = "App";
    public const string? DbSchema = null;
    public const string AdminEmailDefaultValue = IdentityDataSeedContributor.AdminEmailDefaultValue;
    public const string AdminPasswordDefaultValue = IdentityDataSeedContributor.AdminPasswordDefaultValue;
}
public class EntityConfigurationConsts
{
    public const int TaskItemNameMaxLength = 256;
    public const int CategoryNameMaxLength = 128;
}
