using AssetManagementApp.Dtos.BlobFilesDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AssetManagementApp.Interfaces;

public interface IFileAppServices : IApplicationService
{
    Task UploadAsync(UploadFileDto input);
    Task<List<string>> ListFilesAsync();
    Task<byte[]> DownloadAsync(string fileName);
    Task DeleteAsync(string fileName);
}
