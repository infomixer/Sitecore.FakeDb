namespace Sitecore.FakeDb.AutoXunit
{
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using Ploeh.AutoFixture;
  using Sitecore.Data.Serialization.ObjectModel;
  using Sitecore.Diagnostics;
  using Sitecore.FakeDb.Serialization;

  public class AutoDbCustomization : ICustomization
  {
    private readonly string path;

    public AutoDbCustomization()
    {
    }

    public AutoDbCustomization(string path)
    {
      this.path = path;
    }

    public void Customize(IFixture fixture)
    {
      Assert.ArgumentNotNull(fixture, "fixture");

      const string serializationFolderName = "master";

      var templates =
        GetSerializationInfo(serializationFolderName, "/sitecore/templates", true)
          .Where(si => si.Key.TemplateID == TemplateIDs.Template.ToString())
          .Select(si => new DsDbTemplate(serializationFolderName, si.Key, si.Value));

      var items =
        GetSerializationInfo(serializationFolderName, "/sitecore/content", false)
          .Where(si => si.Key.TemplateID != TemplateIDs.Template.ToString())
          .Select(si => new DsDbItem(serializationFolderName, si.Key, si.Value, true));

      fixture.Customize<Db>(d => d.Do(x => { Add(x, templates); Add(x, items); }));
    }

    private static IEnumerable<KeyValuePair<SyncItem, FileInfo>> GetSerializationInfo(string serializationFolderName, string root, bool recursive)
    {
      var serializationFolder = Deserializer.GetSerializationFolder(serializationFolderName);
      var truePath = ShortenedPathsDictionary.FindTruePath(serializationFolder, root);

      var folder =
        new DirectoryInfo(
          Path.Combine(
            serializationFolder.FullName.Trim(new[] { Path.DirectorySeparatorChar }),
            truePath.Replace('/', Path.DirectorySeparatorChar).Trim(new[] { Path.DirectorySeparatorChar })));

      return folder.Deserialize(recursive);
    }

    private static void Add(Db db, IEnumerable<DbItem> items)
    {
      foreach (var item in items)
      {
        db.Add(item);
      }
    }
  }
}