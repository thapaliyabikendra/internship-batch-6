using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Asset_Management_App.Pages;

[Collection(Asset_Management_AppTestConsts.CollectionDefinitionName)]
public class Index_Tests : Asset_Management_AppWebTestBase
{
    [Fact]
    public async Task Welcome_Page()
    {
        var response = await GetResponseAsStringAsync("/");
        response.ShouldNotBeNull();
    }
}
