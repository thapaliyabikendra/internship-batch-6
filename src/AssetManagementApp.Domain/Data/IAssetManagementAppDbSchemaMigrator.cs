using System.Threading.Tasks;

namespace AssetManagementApp.Data;

public interface IAssetManagementAppDbSchemaMigrator
{
    Task MigrateAsync();
}
