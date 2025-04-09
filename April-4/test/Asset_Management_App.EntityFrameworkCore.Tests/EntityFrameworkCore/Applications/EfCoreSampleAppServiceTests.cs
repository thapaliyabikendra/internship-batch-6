using Asset_Management_App.Samples;
using Xunit;

namespace Asset_Management_App.EntityFrameworkCore.Applications;

[Collection(Asset_Management_AppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<Asset_Management_AppEntityFrameworkCoreTestModule>
{

}
