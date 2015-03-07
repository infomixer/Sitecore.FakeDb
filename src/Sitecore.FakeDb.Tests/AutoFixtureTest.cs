namespace Sitecore.FakeDb.Tests
{
  using FluentAssertions;
  using Ploeh.AutoFixture;
  using Xunit;

  public class AutoFixtureTest
  {
    [Fact]
    public void ShouldDeserializeItems()
    {
      // arrange
      var fixture = new Fixture();
      fixture.Customize<Db>(d => d.Do(x => x.Add(new DbItem("home"))));

      // act
      using (var db = fixture.Create<Db>())
      {
        // assert
        db.GetItem("/sitecore/content/home").Should().NotBeNull();
      }
    }
  }
}