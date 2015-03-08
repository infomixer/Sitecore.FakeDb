namespace Sitecore.FakeDb.Serialization.Tests.AutoXunit
{
  using System;
  using Sitecore.FakeDb.AutoXunit;
  using Xunit;

  public class AutoDbCustomizationTest
  {
    [Fact]
    public void ShouldThrowIfFixtureIsNull()
    {
      // act & assert
      Assert.Throws<ArgumentNullException>(() => new AutoDbCustomization().Customize(null));
    }
  }
}