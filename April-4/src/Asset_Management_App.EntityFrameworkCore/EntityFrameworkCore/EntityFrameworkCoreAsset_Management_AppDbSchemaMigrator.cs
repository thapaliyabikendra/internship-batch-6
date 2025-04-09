using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Asset_Management_App.Data;
using Volo.Abp.DependencyInjection;

namespace Asset_Management_App.EntityFrameworkCore;

public class EntityFrameworkCoreAsset_Management_AppDbSchemaMigrator
    : IAsset_Management_AppDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreAsset_Management_AppDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the Asset_Management_AppDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<Asset_Management_AppDbContext>()
            .Database
            .MigrateAsync();
    }
}
