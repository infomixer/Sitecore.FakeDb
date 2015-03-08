namespace Sitecore.FakeDb.AutoXunit
{
  using Ploeh.AutoFixture;
  using Ploeh.AutoFixture.Xunit;

  public class AutoDbAttribute : AutoDataAttribute
  {
    public AutoDbAttribute()
      : base(new Fixture().Customize(new AutoDbCustomization()))
    {
    }
  }
}