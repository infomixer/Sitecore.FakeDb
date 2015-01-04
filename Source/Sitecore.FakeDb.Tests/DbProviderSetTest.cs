namespace Sitecore.FakeDb.Tests
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Common;
  using Sitecore.FakeDb.Tests.Common;
  using Sitecore.Security.Authentication;
  using System;
  using Xunit;

  public class DbProviderSetTest
  {
    private readonly IProviderSwitcherFactory switcherFactory;

    private readonly DbProviderSet providerSet;

    public DbProviderSetTest()
    {
      this.switcherFactory = Substitute.For<IProviderSwitcherFactory>();
      this.providerSet = new DbProviderSet(switcherFactory);
    }

    [Fact]
    public void ShouldHaveNoRegisteredProvidersByDefault()
    {
      // assert
      this.providerSet.Providers.Should().BeEmpty();
    }

    [Fact]
    public void ShouldRegisterDbProvider()
    {
      // arrange
      var providerType = typeof(SampleProvider);
      var switcherType = typeof(SampleProviderSwitcher);

      // act
      this.providerSet.RegisterSwitcher(providerType, switcherType);

      // assert
      this.providerSet.Providers.Should().ContainKey(providerType);
      this.providerSet.Providers[providerType].Should().BeSameAs(switcherType);
    }

    [Fact]
    public void ShouldUseLatestRegistration()
    {
      // arrange
      var providerType = typeof(SampleProvider);
      var switcherType1 = typeof(Switcher<SampleProvider>);
      var switcherType2 = typeof(SampleProviderSwitcher);

      // act
      this.providerSet.RegisterSwitcher(providerType, switcherType1);
      this.providerSet.RegisterSwitcher(providerType, switcherType2);

      // assert
      this.providerSet.Providers.Should().ContainKey(providerType);
      this.providerSet.Providers[providerType].Should().BeSameAs(switcherType2);
    }

    [Fact]
    public void ShouldThrowExceptionIfNoSwitcherRegistered()
    {
      // arrange
      var provider = Substitute.For<SampleProvider>();

      // act
      Action action = () => this.providerSet.Switch<SampleProvider>(provider);

      // assert
      action
        .ShouldThrow<InvalidOperationException>()
        .WithMessage("Unable to switch the provider of type 'Sitecore.FakeDb.Tests.Common.SampleProvider'. The switcher has not been registered.");
    }

    [Fact]
    public void ShouldSwitchProvider()
    {
      // arrange
      var providerType = typeof(SampleProvider);
      var switcherType = typeof(SampleProviderSwitcher);

      var provider = Substitute.For<SampleProvider>();
      var switcher = Substitute.For<SampleProviderSwitcher>();

      this.switcherFactory
        .Create(switcherType, provider)
        .Returns(switcher);

      this.providerSet.RegisterSwitcher(providerType, switcherType);

      // act
      this.providerSet.Switch<SampleProvider>(provider);

      // assert
      this.switcherFactory
        .Received()
        .Create(switcherType, provider);
    }

    [Fact]
    public void ShouldBeDisposable()
    {
      // assert
      this.providerSet.Should().BeAssignableTo<IDisposable>();
    }

    [Fact]
    public void ShouldDisposeSwitcher()
    {
      // arrange
      var providerType = typeof(SampleProvider);
      var switcherType = typeof(SampleProviderSwitcher);

      var provider = Substitute.For<SampleProvider>();
      var switcher = Substitute.For<SampleProviderSwitcher>();

      this.switcherFactory
        .Create(switcherType, provider)
        .Returns(switcher);

      this.providerSet.RegisterSwitcher(providerType, switcherType);
      this.providerSet.Switch<SampleProvider>(provider);

      // act
      this.providerSet.Dispose();

      // assert
      switcher.Received().Dispose();
    }
  }
}