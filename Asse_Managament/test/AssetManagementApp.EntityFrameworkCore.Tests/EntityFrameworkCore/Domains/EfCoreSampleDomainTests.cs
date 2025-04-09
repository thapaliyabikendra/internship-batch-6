using AssetManagementApp.Samples;
using Xunit;

namespace AssetManagementApp.EntityFrameworkCore.Domains;

[Collection(AssetManagementAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<AssetManagementAppEntityFrameworkCoreTestModule>
{

}
