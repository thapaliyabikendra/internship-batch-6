using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AssetManagementApp.AppServices;
using AssetManagementApp.Dtos.BlobFilesDtos;
using AssetManagementApp.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc;
using Volo.Abp.BlobStoring;


namespace AssetManagementApp.Controllers;

[Route("api/file")]
public class FileController : AssetManagementAppController
{
    private readonly IFileAppServices _fileAppService;

    public FileController(IFileAppServices fileAppServices, IAssetAppService assetAppService) : base(assetAppService)
    {
        _fileAppService = fileAppServices;
    }
    
    [HttpPost]
    public async Task<IActionResult> Upload([FromBody] UploadFileDto input)
    {
        await _fileAppService.UploadAsync(input);
        return Ok("Uploaded successfully.");
    }

    [HttpGet("{fileName}")]
    public async Task<IActionResult> Download(string fileName)
    {
        var bytes = await _fileAppService.DownloadAsync(fileName);
        return File(bytes, "application/octet-stream", fileName);
    }

    [HttpDelete("{fileName}")]
    public async Task<IActionResult> Delete(string fileName)
    {
        await _fileAppService.DeleteAsync(fileName);
        return Ok("Deleted successfully.");
    }
}
