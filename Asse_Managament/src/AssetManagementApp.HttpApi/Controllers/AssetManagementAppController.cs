using AssetManagementApp.Interfaces;
using AssetManagementApp.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Mvc;

namespace AssetManagementApp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class AssetManagementAppController : AbpControllerBase
{
    private readonly IAssetAppService _assetAppService;
    protected AssetManagementAppController(IAssetAppService assetAppService)
    {
        _assetAppService = assetAppService;
        LocalizationResource = typeof(AssetManagementAppResource);
    }

    [HttpGet]
    [Route("api/assets/test-redis")]
    public async Task<IActionResult> TestRedis()
    {
        await _assetAppService.TestRedisCacheAsync();
        return Ok("Redis test completed.");
    }
}
