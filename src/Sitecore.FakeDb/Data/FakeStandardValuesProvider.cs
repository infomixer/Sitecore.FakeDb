namespace Sitecore.FakeDb.Data
{
  using System.Linq;
  using System.Threading;
  using Sitecore.Data;
  using Sitecore.Data.Fields;
  using Sitecore.Data.Items;
  using Sitecore.Diagnostics;
  using Sitecore.FakeDb.Data.Engines;

  public class FakeStandardValuesProvider : StandardValuesProvider, IRequireDataStorage
  {
    private readonly ThreadLocal<DataStorage> storage;

    public FakeStandardValuesProvider()
    {
      this.storage = new ThreadLocal<DataStorage>();
    }

    public virtual DataStorage DataStorage
    {
      get { return this.storage.Value; }
    }

    public override string GetStandardValue(Field field)
    {
      var templateId = field.Item.TemplateID;

      Assert.IsNotNull(this.DataStorage, "DataStorage cannot be null.");

      var template = this.DataStorage.GetFakeTemplate(templateId);

      if (template == null)
      {
        // This will be the case for things like TemplateFieldItem.Type. 
        // We don't have those templates and it's safe to just return an empty string
        return string.Empty;
      }

      var standardValue = this.FindStandardValueInTheTemplate(template, field.ID) ?? string.Empty;

      return this.ReplaceTokens(standardValue, field.Item);
    }

    public virtual void SetDataStorage(DataStorage dataStorage)
    {
      Assert.ArgumentNotNull(dataStorage, "dataStorage");

      this.storage.Value = dataStorage;
    }

    protected string ReplaceTokens(string standardValue, Item item)
    {
      Assert.IsNotNull(standardValue, "standardValue");
      Assert.IsNotNull(item, "item");

      return standardValue.Replace("$name", item.Name);
    }

    protected string FindStandardValueInTheTemplate(DbTemplate template, ID fieldId)
    {
      if (template.StandardValues.InnerFields.ContainsKey(fieldId))
      {
        return template.StandardValues[fieldId].Value;
      }

      if (template.BaseIDs == null || template.BaseIDs.Length <= 0)
      {
        return null;
      }

      var dataStorage = this.DataStorage;
      return template.BaseIDs.Select(dataStorage.GetFakeTemplate)
                             .Select(baseTemplate => this.FindStandardValueInTheTemplate(baseTemplate, fieldId)).FirstOrDefault(value => value != null);
    }
  }
}