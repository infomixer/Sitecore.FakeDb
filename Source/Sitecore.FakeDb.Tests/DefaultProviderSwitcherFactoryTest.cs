namespace Sitecore.FakeDb.Tests
{
  using FluentAssertions;
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
  }
}
