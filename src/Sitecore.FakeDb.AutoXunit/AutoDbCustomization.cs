namespace Sitecore.FakeDb.AutoXunit
{
  using Ploeh.AutoFixture;
  using Sitecore.FakeDb.Serialization;

  public class AutoDbCustomization : ICustomization
  {
    public void Customize(IFixture fixture)
    {
      fixture.Customize<Db>(d => d.Do(x => x.Add(new DsDbItem("/sitecore/content/home"))));
    }
  }
}