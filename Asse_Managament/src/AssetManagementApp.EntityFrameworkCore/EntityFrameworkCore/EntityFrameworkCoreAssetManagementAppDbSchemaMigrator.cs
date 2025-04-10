using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using AssetManagementApp.Data;
using Volo.Abp.DependencyInjection;

namespace AssetManagementApp.EntityFrameworkCore;

public class EntityFrameworkCoreAssetManagementAppDbSchemaMigrator
    : IAssetManagementAppDbSchemaMigrator, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public EntityFrameworkCoreAssetManagementAppDbSchemaMigrator(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task MigrateAsync()
    {
        /* We intentionally resolving the AssetManagementAppDbContext
         * from IServiceProvider (instead of directly injecting it)
         * to properly get the connection string of the current tenant in the
         * current scope.
         */

        await _serviceProvider
            .GetRequiredService<AssetManagementAppDbContext>()
            .Database
            .MigrateAsync();
    }
}
