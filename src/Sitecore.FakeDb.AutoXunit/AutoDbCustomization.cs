namespace Sitecore.FakeDb.AutoXunit
{
  using Ploeh.AutoFixture;
  using Sitecore.Diagnostics;
  using Sitecore.FakeDb.Serialization;

  public class AutoDbCustomization : ICustomization
  {
    private readonly string path;

    public AutoDbCustomization()
    {
    }

    public AutoDbCustomization(string path)
    {
      this.path = path;
    }

    public void Customize(IFixture fixture)
    {
      Assert.ArgumentNotNull(fixture, "fixture");

      fixture.Customize<Db>(d => d.Do(x => x.Add(new DsDbItem(this.path, true))));
    }
  }
}