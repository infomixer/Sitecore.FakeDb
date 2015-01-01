namespace Sitecore.FakeDb
{
  using Sitecore.Diagnostics;
  using System;
  using System.Collections.Generic;

  public class DbProviderSet
  {
    private readonly IDictionary<Type, Type> providers = new Dictionary<Type, Type>();

    private readonly IProviderSwitcherFactory switcherFactory;

    public DbProviderSet()
    {
    }

    public DbProviderSet(IProviderSwitcherFactory switcherFactory)
    {
      Assert.ArgumentNotNull(switcherFactory, "switcherFactory");

      this.switcherFactory = switcherFactory;
    }

    public IDictionary<Type, Type> Providers
    {
      get { return providers; }
    }

    public virtual void Register(Type providerType, Type switcherType)
    {
      Assert.ArgumentNotNull(providerType, "providerType");
      Assert.ArgumentNotNull(switcherType, "switcherType");

      this.providers[providerType] = switcherType;
    }

    public virtual void Switch<TProvider>(TProvider provider)
    {
      Assert.ArgumentNotNull(provider, "provider");

      var providerType = typeof(TProvider);
      Assert.IsTrue(
        this.providers.ContainsKey(providerType),
        "Unable to switch the provider of type '{0}'. The switcher has not been registered.",
        providerType);

      var switcherType = this.providers[providerType];

      this.switcherFactory.Create(switcherType, provider);
    }
  }
}