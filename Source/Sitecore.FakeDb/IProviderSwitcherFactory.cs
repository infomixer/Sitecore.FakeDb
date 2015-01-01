namespace Sitecore.FakeDb
{
  using System;

  public interface IProviderSwitcherFactory
  {
    IDisposable Create(Type switcherType, object provider);
  }
}