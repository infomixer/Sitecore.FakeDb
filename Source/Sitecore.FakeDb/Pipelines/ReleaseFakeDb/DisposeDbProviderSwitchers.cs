namespace Sitecore.FakeDb.Pipelines.ReleaseFakeDb
{
  using Sitecore.Diagnostics;

  public class DisposeDbProviderSwitchers
  {
    public virtual void Process(ReleaseDbArgs args)
    {
      Assert.ArgumentNotNull(args, "args");

      if (args.Db.Providers == null)
      {
        return;
      }

      args.Db.Providers.Dispose();
    }
  }
}
