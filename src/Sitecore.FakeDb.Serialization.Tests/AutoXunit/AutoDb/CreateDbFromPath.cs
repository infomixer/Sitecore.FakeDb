namespace Sitecore.FakeDb.Serialization.Tests.AutoXunit.AutoDb
{
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.FakeDb.AutoXunit;
  using Xunit;
  using Xunit.Extensions;

  [Trait("AutoDb", "Load entire database (default)")]
  public class CreateDefault
  {
    [AutoDb]
    [Theory(DisplayName = @"Database is ""master""")]
    public void DatabaseMaster(Db db)
    {
      db.Database.Name.Should().Be("master");
    }

    [AutoDb]
    [Theory(DisplayName = "Content items are loaded")]
    public void ContentItemsLoaded(Db db)
    {
      db.GetItem("/sitecore/content/Home/Child Item/Grandchild Item").Should().NotBeNull();
    }

    [AutoDb]
    [Theory(DisplayName = "Templates are loaded")]
    public void TemplatesLoaded(Db db)
    {
      db.Database.GetTemplate(Constants.SomeTemplateId).Should().NotBeNull();
    }
  }

  [Trait("AutoDb", "Load single item by path")]
  public class LoadSingleItemByPath
  {
    private const string SitecoreContentHome = "/sitecore/content/home";

    [AutoDb(SitecoreContentHome)]
    [Theory(DisplayName = "Item is loaded")]
    public void ItemLoaded(Db db)
    {
      db.GetItem(SitecoreContentHome).Should().NotBeNull();
    }

    [AutoDb(SitecoreContentHome)]
    [Theory(DisplayName = "Children are not loaded", Skip = "To be implemented")]
    public void ChildrenNotLoaded(Db db)
    {
      db.GetItem(SitecoreContentHome).Children.Should().BeEmpty();
    }

    [AutoDb(SitecoreContentHome)]
    [Theory(DisplayName = "Template is loaded")]
    public void TemplateLoaded(Db db)
    {
      db.GetItem(SitecoreContentHome).Template.InnerItem.Languages.Length.Should().BeGreaterThan(3);
    }

    [AutoDb(SitecoreContentHome)]
    [Theory(DisplayName = "Children templates are not loaded", Skip = "To be implemented")]
    public void ChildrenTemplatesNotLoaded(Db db)
    {
      db.Database.GetTemplate(Constants.SomeTemplateId).Should().BeNull();
    }
  }

  [Trait("AutoDb", "Load tree by path")]
  public class LoadTreeByPath
  {
    private const string SitecoreContentHome = "/sitecore/content/home";

    [AutoDb(SitecoreContentHome)]
    [Theory(DisplayName = "Root item is loaded")]
    public void RootItemLoaded(Db db)
    {
      db.GetItem(SitecoreContentHome).Should().NotBeNull();
    }

    [AutoDb(SitecoreContentHome)]
    [Theory(DisplayName = "Children are loaded")]
    public void ChildrenLoaded(Db db)
    {
      db.GetItem(SitecoreContentHome).Children.Should().NotBeEmpty();
    }

    [AutoDb(SitecoreContentHome)]
    [Theory(DisplayName = "Root item template is loaded")]
    public void RootItemTemplateLoaded(Db db)
    {
      db.GetItem(SitecoreContentHome).Template.InnerItem.Languages.Length.Should().BeGreaterThan(3);
    }

    [AutoDb(SitecoreContentHome)]
    [Theory(DisplayName = "Children templates are loaded")]
    public void ChildrenTemplatesLoaded(Db db)
    {
      db.Database.GetTemplate(Constants.SomeTemplateId).Should().NotBeNull();
    }
  }

  public static class Constants
  {
    public static ID SomeTemplateId = new ID("{F6A72DBF-558F-40E5-8033-EE4ACF027FE2}");
  }
}