using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace AssetManagementApp.AppServices;

public class TestAppServices : ApplicationService
{
    private readonly MySettings _mySettings;

    public TestAppServices(IOptions<MySettings> options)
    {
        _mySettings = options.Value;
    }

    public string GetAppSettings()
    {
        return $"Title: {_mySettings.Title}, Version: {_mySettings.Version}, Mode: {_mySettings.Mode}, Description: {_mySettings.Description}";
    }
}
