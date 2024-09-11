using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Banga.Tests.Unit
{
    [CollectionDefinition("Fixture collection")]
    public class TestCollection : ICollectionFixture<CustomWebApplicationFactory<Program>>
    {
    }
}
