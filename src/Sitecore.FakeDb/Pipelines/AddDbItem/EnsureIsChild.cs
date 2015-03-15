namespace Sitecore.FakeDb.Pipelines.AddDbItem
{
  using Sitecore.Diagnostics;

  public class EnsureIsChild
  {
    public virtual void Process(AddDbItemArgs args)
    {
      var item = args.DbItem;
      var dataStorage = args.DataStorage;

      var parent = dataStorage.GetFakeItem(item.ParentID);
      Assert.IsNotNull(parent, "Parent not found for item \"{0}\", \"{1}\"", item.Name, item.ID);

      if (!parent.Children.Contains(item))
      {
        parent.Children.Add(item);
      }
    }
  }
}