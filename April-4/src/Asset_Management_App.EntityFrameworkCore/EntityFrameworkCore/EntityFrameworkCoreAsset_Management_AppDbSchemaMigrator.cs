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

    public Task MigrateAsync()
    {
        throw new NotImplementedException();
    }
}
