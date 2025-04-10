using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace AssetManagementApp.Data;

/* This is used if database provider does't define
 * IAssetManagementAppDbSchemaMigrator implementation.
 */
public class NullAssetManagementAppDbSchemaMigrator : IAssetManagementAppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
