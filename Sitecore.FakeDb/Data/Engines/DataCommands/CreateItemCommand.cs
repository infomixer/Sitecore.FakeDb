﻿namespace Sitecore.FakeDb.Data.Engines.DataCommands
{
  using Sitecore.Data;
  using Sitecore.Data.Items;
  using Sitecore.FakeDb.Data.Items;

  public class CreateItemCommand : Sitecore.Data.Engines.DataCommands.CreateItemCommand
  {
    protected override Sitecore.Data.Engines.DataCommands.CreateItemCommand CreateInstance()
    {
      return new CreateItemCommand();
    }

    protected override Item DoExecute()
    {
      var item = ItemHelper.CreateInstance(ItemName, ItemId, TemplateId, new FieldList(), Database);

      var dataStorage = CommandHelper.GetDataStorage(this);

      var fullPath = Destination.Paths.FullPath + "/" + ItemName;
      dataStorage.FakeItems.Add(ItemId, new FItem(ItemName, ItemId, TemplateId) { ParentID = Destination.ID, FullPath = fullPath });
      
      dataStorage.Items.Add(ItemId, item);

      return item;
    }
  }
}