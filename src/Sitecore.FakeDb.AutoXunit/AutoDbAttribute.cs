namespace Sitecore.FakeDb.AutoXunit
{
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.Xunit;

  public class AutoDbAttribute : AutoDataAttribute
  {
    // TODO: default item should be /sitecore
    public AutoDbAttribute()
      : this("/sitecore/content/home")
    {
    }

    public AutoDbAttribute(string path)
      : base(new Fixture().Customize(new AutoDbCustomization(path)))
    {
    }
  }
}