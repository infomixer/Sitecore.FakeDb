namespace Sitecore.FakeDb.Tests.Common
{
  using Sitecore.Common;

  public class SampleProviderSwitcher : Switcher<SampleProvider>
  {
    public SampleProviderSwitcher()
    {
    }

    public SampleProviderSwitcher(SampleProvider provider)
      : base(provider)
    {
    }
  }
}
