namespace Sitecore.FakeDb.Tests
{
  using FluentAssertions;
  using Sitecore.Common;
  using Sitecore.Security.Authentication;
  using Xunit;

  public class DbProviderSetTest
  {
    private readonly DbProviderSet providerSet = new DbProviderSet();

    [Fact]
    public void ShouldHaveNoRegisteredProvidersByDefault()
    {
      // assert
      providerSet.Providers.Should().BeEmpty();
    }

    [Fact]
    public void ShouldRegisterDbProvider()
    {
      // arrange
      var providerType = typeof(AuthenticationProvider);
      var switcherType = typeof(AuthenticationSwitcher);

      // act
      providerSet.Register(providerType, switcherType);

      // assert
      providerSet.Providers.Should().ContainKey(providerType);
      providerSet.Providers[providerType].Should().BeSameAs(switcherType);
    }

    [Fact]
    public void ShouldUseLatestRegistration()
    {
      // arrange
      var providerType = typeof(AuthenticationProvider);
      var switcherType1 = typeof(Switcher<AuthenticationProvider>);
      var switcherType2 = typeof(AuthenticationSwitcher);

      // act
      providerSet.Register(providerType, switcherType1);
      providerSet.Register(providerType, switcherType2);

      // assert
      providerSet.Providers.Should().ContainKey(providerType);
      providerSet.Providers[providerType].Should().BeSameAs(switcherType2);
    }
  }
}
