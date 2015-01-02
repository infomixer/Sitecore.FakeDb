namespace Sitecore.FakeDb
{
  using Sitecore.Diagnostics;
  using Sitecore.Reflection;
  using System;

  public class DefaultProviderSwitcherFactory : IProviderSwitcherFactory
  {
    public IDisposable Create(Type switcherType, object provider)
    {
      Assert.ArgumentNotNull(switcherType, "switcherType");
      Assert.ArgumentNotNull(provider, "provider");

      return (IDisposable)ReflectionUtil.CreateObject(switcherType, new[] { provider });
    }
  }
}
