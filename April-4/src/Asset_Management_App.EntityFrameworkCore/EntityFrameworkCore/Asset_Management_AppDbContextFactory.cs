using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Asset_Management_App.EntityFrameworkCore;

/* This class is needed for EF Core console commands
 * (like Add-Migration and Update-Database commands) */
public class Asset_Management_AppDbContextFactory : IDesignTimeDbContextFactory<Asset_Management_AppDbContext>
{
    public Asset_Management_AppDbContext CreateDbContext(string[] args)
    {
        var configuration = BuildConfiguration();
        
        Asset_Management_AppEfCoreEntityExtensionMappings.Configure();

        var builder = new DbContextOptionsBuilder<Asset_Management_AppDbContext>()
            .UseSqlServer(configuration.GetConnectionString("Default"));
        
        return new Asset_Management_AppDbContext(builder.Options);
    }

    private static IConfigurationRoot BuildConfiguration()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../Asset_Management_App.DbMigrator/"))
            .AddJsonFile("appsettings.json", optional: false);

        return builder.Build();
    }
}
