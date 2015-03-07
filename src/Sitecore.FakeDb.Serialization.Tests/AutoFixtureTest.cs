namespace Sitecore.FakeDb.Serialization.Tests
{
  using FluentAssertions;
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.Xunit;
  using Xunit.Extensions;

  public class AutoFixtureTest
  {
    [Theory, AutoDb]
    public void ShouldDeserializeItems(Db db)
    {
      // act
      var item = db.GetItem("/sitecore/content/home");

      // assert
      item.Should().NotBeNull();
      item["Title"].Should().Be("Sitecore");
    }
  }

  public class AutoDbAttribute : AutoDataAttribute
  {
    public AutoDbAttribute()
      : base(new Fixture().Customize(new AutoDbCustomization()))
    {
    }
  }

  public class AutoDbCustomization : ICustomization
  {
    public void Customize(IFixture fixture)
    {
      fixture.Customize<Db>(d => d.Do(x => x.Add(new DsDbItem("/sitecore/content/home"))));
    }
  }
}