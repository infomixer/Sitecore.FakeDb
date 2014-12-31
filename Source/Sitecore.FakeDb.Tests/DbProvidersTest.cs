namespace Sitecore.FakeDb.Tests
{
  using FluentAssertions;
  using Xunit;

  public class DbProvidersTest
  {
    [Fact]
    public void ShouldInstantiateDbProviders()
    {
      using (var db = new Db())
      {
        db.Providers.Should().NotBeNull();
      }
    }
  }
}
