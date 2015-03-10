namespace Sitecore.FakeDb.AutoXunit
{
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.Xunit;

  public class AutoDbAttribute : AutoDataAttribute
  {
    // TODO:[High] Remove sitecore.item and content.item serialization files.
    public AutoDbAttribute()
      : this("/sitecore")
    {
    }

    public AutoDbAttribute(string path)
      : base(new Fixture().Customize(new AutoDbCustomization(path)))
    {
    }
  }
}