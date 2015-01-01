namespace Sitecore.FakeDb.Tests
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Common;
  using Sitecore.Security.Authentication;
  using System;
  using Xunit;

  public class DbProviderSetTest
  {
    private readonly DbProviderSet providerSet;

    public DbProviderSetTest()
    {
      this.providerSet = new DbProviderSet();
    }

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

    [Fact]
    public void ShouldThrowExceptionIfNoSwitcherRegistered()
    {
      // arrange
      var provider = Substitute.For<AuthenticationProvider>();

      // act
      Action action = () => providerSet.Switch<AuthenticationProvider>(provider);

      // assert
      action
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("Unable to switch the provider of type 'Sitecore.Security.Authentication.AuthenticationProvider'. The switcher has not been registered.");
    }

    [Fact]
    public void ShouldSwitchProvider()
    {
      // arrange
      var providerType = typeof(AuthenticationProvider);
      var switcherType = typeof(AuthenticationSwitcher);

      var provider = Substitute.For<AuthenticationProvider>();
      var switcher = Substitute.For<AuthenticationSwitcher>(provider);

      var switcherFactory = Substitute.For<IProviderSwitcherFactory>();
      switcherFactory
        .Create(switcherType, provider)
        .Returns(switcher);

      var providerSet = new DbProviderSet(switcherFactory);
      providerSet.Register(providerType, switcherType);

      // act
      providerSet.Switch<AuthenticationProvider>(provider);

      // assert
      switcherFactory
        .Received()
        .Create(switcherType, provider);
    }
  }
}