namespace Sitecore.FakeDb
{
  using System;
  using System.Collections.Generic;

  public class DbProviderSet
  {
    private readonly IDictionary<Type, Type> providers = new Dictionary<Type, Type>();

    public IDictionary<Type, Type> Providers
    {
      get { return providers; }
    }

    public virtual void Register(Type providerType, Type switcherType)
    {
    }
  }
}
