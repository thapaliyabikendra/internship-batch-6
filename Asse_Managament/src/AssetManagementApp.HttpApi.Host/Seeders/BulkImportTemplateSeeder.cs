using AssetManagementApp.Dtos.BlobFilesDtos;
using AssetManagementApp.Interfaces;
using Castle.Core.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace AssetManagementApp.Seeders;

public class BulkImportTemplateSeeder : IDataSeedContributor, ITransientDependency
{
    private readonly IServiceProvider _serviceProvider;

    public BulkImportTemplateSeeder(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task SeedAsync(DataSeedContext context)
    {
        using var scope = _serviceProvider.CreateScope();

        var fileAppService = scope.ServiceProvider.GetRequiredService<IFileAppServices>();

        // define the path 
        var templatePath = Path.Combine(
            Directory.GetCurrentDirectory(),
            "wwwroot",
            "templates",
            "BulkImportTemplate.xlsx"
        );

        // check it the file exists
        if(File.Exists(templatePath))
        {
            var fileBytes = File.ReadAllBytesAsync(templatePath);

            await fileAppService.UploadAsync(new UploadFileDto
            {
                Name = "BulkImportTemplate.xlsx",
                FileContent = fileBytes.Result
            });

            //_logger.LogDebug("File uploaded to blob.");
        }
        else
        {
            throw new FileNotFoundException("The bulk import template file was not found.", templatePath);
        }
    }

    internal async Task SeedAsync()
    {
        throw new NotImplementedException();
    }
}
