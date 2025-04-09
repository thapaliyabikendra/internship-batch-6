using System.Threading.Tasks;

namespace Asset_Management_App.Data;

public interface IAsset_Management_AppDbSchemaMigrator
{
    Task MigrateAsync();
}
