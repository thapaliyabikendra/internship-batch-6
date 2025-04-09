using Xunit;

namespace AssetManagementApp.EntityFrameworkCore;

[CollectionDefinition(AssetManagementAppTestConsts.CollectionDefinitionName)]
public class AssetManagementAppEntityFrameworkCoreCollection : ICollectionFixture<AssetManagementAppEntityFrameworkCoreFixture>
{

}
