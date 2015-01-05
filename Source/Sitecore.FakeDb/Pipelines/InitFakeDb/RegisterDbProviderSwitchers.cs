namespace Sitecore.FakeDb.Pipelines.InitFakeDb
{
  using System;
  using System.Linq;
  using Sitecore.Data.IDTables;
  using Sitecore.Diagnostics;
  using Sitecore.FakeDb.Data.IDTables;
  using Sitecore.Security.Authentication;
  using Sitecore.Common;
  using System.Reflection;

  public class RegisterDbProviderSwitchers
  {
    public virtual void Process(InitDbArgs args)
    {
      Assert.ArgumentNotNull(args, "args");

      if (args.Providers == null)
      {
        return;
      }

      this.RegisterThreadLocalProviderSwitchers(args);
      this.RegisterCommonProviderSwitchers(args);
    }

    protected virtual void RegisterThreadLocalProviderSwitchers(InitDbArgs args)
    {
      this.RegisterSwitchers(args.Providers, typeof(ThreadLocalProviderSwitcher<>));
    }

    public virtual void RegisterCommonProviderSwitchers(InitDbArgs args)
    {
      this.RegisterSwitchers(args.Providers, typeof(Switcher<>));
    }

    protected void RegisterSwitchers(DbProviderSet providers, Type baseType)
    {
      var switcherTypes =
        baseType.Assembly
          .GetTypes()
          .Where(t => t.IsVisible && t.BaseType != null && t.BaseType.IsGenericType &&
                      t.BaseType.GetGenericTypeDefinition() == baseType);

      foreach (var switcherType in switcherTypes)
      {
        var providerType = switcherType.BaseType.GetGenericArguments().FirstOrDefault();
        if (providerType == null)
        {
          continue;
        }

        providers.RegisterSwitcher(providerType, switcherType);
      }
    }
  }
}