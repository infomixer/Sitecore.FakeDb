namespace Sitecore.FakeDb.Tests
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Links;
  using Xunit;

  public class DbProviderSwitcherTest
  {
    [Fact]
    public void ShouldSwitchLinkDatabase()
    {
      // arrange
      var provider = Substitute.For<LinkDatabase>();

      using (var db = new Db())
      {
        var brokenLinks = new ItemLink[] { };
        provider.GetBrokenLinks(db.Database).Returns(brokenLinks);

        // act
        db.Providers.Switch<LinkDatabase>(provider);

        // assert
        Globals.LinkDatabase.GetBrokenLinks(db.Database).Should().BeSameAs(brokenLinks);
      }
    }
  }
}