namespace Sitecore.FakeDb.Tests
{
  using FluentAssertions;
  using Sitecore.Security.Authentication;
  using Xunit;

  public class DbProviderSetTest
  {
    // TODO: Move
    [Fact]
    public void ShouldInstantiateDbProviders()
    {
      using (var db = new Db())
      {
        db.Providers.Should().NotBeNull();
      }
    }

    [Fact]
    public void ShouldHaveNoRegisteredProvidersByDefault()
    {
      var providerSet = new DbProviderSet();

      providerSet.Providers.Should().BeEmpty();
    }

    [Fact]
    public void ShouldRegisterDbProvider()
    {
      var providers = new DbProviderSet();
      var providerType = typeof(AuthenticationProvider);
      var switcherType = typeof(AuthenticationSwitcher);

      providers.Register(providerType, switcherType);

      // TODO: assert    
    }
  }
}
