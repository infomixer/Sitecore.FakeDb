namespace Sitecore.FakeDb.Pipelines.InitFakeDb
{
  using System.Linq;
  using Sitecore.Data.IDTables;
  using Sitecore.Diagnostics;
  using Sitecore.FakeDb.Data.IDTables;
  using System;

  public class RegisterDbProviderSwitchers
  {
    public virtual void Process(InitDbArgs args)
    {
      Assert.ArgumentNotNull(args, "args");

      if (args.Providers == null)
      {
        return;
      }

      var switcherTypes =
        typeof(ThreadLocalProviderSwitcher<object>).Assembly
          .GetTypes()
          .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                      t.BaseType.GetGenericTypeDefinition() == typeof(ThreadLocalProviderSwitcher<>));

      foreach (var switcherType in switcherTypes)
      {
        var providerType = switcherType.BaseType.GetGenericArguments().FirstOrDefault();
        if (providerType == null)
        {
          continue;
        }

        args.Providers.RegisterSwitcher(providerType, switcherType);
      }
    }
  }
}