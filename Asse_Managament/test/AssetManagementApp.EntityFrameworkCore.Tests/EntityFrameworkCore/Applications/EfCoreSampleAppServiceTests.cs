using AssetManagementApp.Samples;
using Xunit;

namespace AssetManagementApp.EntityFrameworkCore.Applications;

[Collection(AssetManagementAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<AssetManagementAppEntityFrameworkCoreTestModule>
{

}
