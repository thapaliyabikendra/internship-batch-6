using AssetManagementApp.Dtos.BlobFilesDtos;
using AssetManagementApp.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.BlobStoring;

namespace AssetManagementApp.AppServices;

public class FileAppServices : ApplicationService, IFileAppServices
{
    private readonly IBlobContainer _blobContainer;

    public FileAppServices(IBlobContainerFactory blobCOntainerfactory)
    {
        _blobContainer = blobCOntainerfactory.Create("StorageContainer");
    }

    public async Task UploadAsync(UploadFileDto input)
    {
        var fileName = input.Name;
        var fileContent = input.FileContent;

        await _blobContainer.SaveAsync(fileName, fileContent);
    }

    public async Task<byte[]> DownloadAsync(string fileName)
    {
        return await _blobContainer.GetAllBytesAsync(fileName);
    }

    public async Task DeleteAsync(string fileName)
    {
        await _blobContainer.DeleteAsync(fileName);
    }
}
