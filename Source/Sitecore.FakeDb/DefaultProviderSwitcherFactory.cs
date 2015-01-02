namespace Sitecore.FakeDb
{
  using System;

  public class DefaultProviderSwitcherFactory : IProviderSwitcherFactory
  {
    public IDisposable Create(Type switcherType, object provider)
    {
      throw new NotImplementedException();
    }
  }
}
