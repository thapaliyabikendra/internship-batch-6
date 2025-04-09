using Microsoft.AspNetCore.Builder;
using Asset_Management_App;
using Volo.Abp.AspNetCore.TestBase;

var builder = WebApplication.CreateBuilder();
builder.Environment.ContentRootPath = GetWebProjectContentRootPathHelper.Get("Asset_Management_App.Web.csproj"); 
await builder.RunAbpModuleAsync<Asset_Management_AppWebTestModule>(applicationName: "Asset_Management_App.Web");

public partial class Program
{
}
