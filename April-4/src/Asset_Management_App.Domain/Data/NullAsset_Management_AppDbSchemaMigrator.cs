using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Asset_Management_App.Data;

/* This is used if database provider does't define
 * IAsset_Management_AppDbSchemaMigrator implementation.
 */
public class NullAsset_Management_AppDbSchemaMigrator : IAsset_Management_AppDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
