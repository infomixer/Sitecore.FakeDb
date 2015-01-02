namespace Sitecore.FakeDb.Tests
{
  using FluentAssertions;
  using Sitecore.Common;
  using Sitecore.FakeDb.Tests.Common;
  using Xunit;

  public class DefaultProviderSwitcherFactoryTest
  {
    [Fact]
    public void ShouldBeProviderSwitcherFactory()
    {
      // arrange
      var factory = new DefaultProviderSwitcherFactory();

      // assert
      factory.Should().BeAssignableTo<IProviderSwitcherFactory>();
    }

    [Fact]
    public void ShouldCreateSwitcher()
    {
      // arrange
      var factory = new DefaultProviderSwitcherFactory();
      var switcherType = typeof(SampleProviderSwitcher);
      var provider = new SampleProvider();

      // act
      var switcher = factory.Create(switcherType, provider);

      // assert
      switcher.Should().NotBeNull();
      switcher.Should().BeAssignableTo<Switcher<SampleProvider>>();
    }
  }
}
