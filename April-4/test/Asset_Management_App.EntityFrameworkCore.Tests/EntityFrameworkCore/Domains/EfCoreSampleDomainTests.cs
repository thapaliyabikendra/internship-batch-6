using Asset_Management_App.Samples;
using Xunit;

namespace Asset_Management_App.EntityFrameworkCore.Domains;

[Collection(Asset_Management_AppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<Asset_Management_AppEntityFrameworkCoreTestModule>
{

}
