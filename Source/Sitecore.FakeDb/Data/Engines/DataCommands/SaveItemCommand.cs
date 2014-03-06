﻿namespace Sitecore.FakeDb.Data.Engines.DataCommands
{
  using Sitecore.Configuration;
  using Sitecore.Data.Fields;
  using Sitecore.Diagnostics;

  public class SaveItemCommand : Sitecore.Data.Engines.DataCommands.SaveItemCommand, IDataEngineCommand
  {
    private DataEngineCommand innerCommand = DataEngineCommand.NotInitialized;

    public virtual void Initialize(DataEngineCommand command)
    {
      this.innerCommand = command;
    }

    protected override Sitecore.Data.Engines.DataCommands.SaveItemCommand CreateInstance()
    {
      return this.innerCommand.CreateInstance<Sitecore.Data.Engines.DataCommands.SaveItemCommand, SaveItemCommand>();
    }

    protected override bool DoExecute()
    {
      var fakeItem = this.innerCommand.DataStorage.GetFakeItem(Item.ID);

      this.UpdateBasicData(fakeItem);
      this.UpdateFields(fakeItem);

      return true;
    }

    protected virtual void UpdateBasicData(DbItem fakeItem)
    {
      var oldName = fakeItem.Name;
      var newName = this.Item.Name;

      if (oldName == newName)
      {
        return;
      }

      fakeItem.Name = this.Item.Name;
      var fullPath = fakeItem.FullPath;
      if (!string.IsNullOrEmpty(fullPath))
      {
        fakeItem.FullPath = fullPath.Substring(0, fullPath.LastIndexOf(oldName, System.StringComparison.Ordinal)) + newName;
      }
    }

    protected virtual void UpdateFields(DbItem fakeItem)
    {
      foreach (Field field in this.Item.Fields)
      {
        if (fakeItem.Fields.InnerFields.ContainsKey(field.ID))
        {
          fakeItem.Fields[field.ID].Value = field.Value;
        }
        else
        {
          fakeItem.Fields.Add(new DbField(field.Name) { ID = field.ID, Value = field.Value });
        }
      }
    }
  }
}