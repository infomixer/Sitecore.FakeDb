namespace Sitecore.FakeDb.Tests.Data.Items
{
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.FakeDb.Data.Items;
  using Sitecore.Globalization;
  using Xunit;

  public class ItemHelperTest
  {
    [Fact]
    public void ShouldCreateSomeItem()
    {
      // arrange
      var item = ItemHelper.CreateInstance();

      // assert
      item.Name.Should().NotBeEmpty();
      item.ID.Should().NotBeNull();
      item.TemplateID.Should().NotBeNull();
      item.Database.Should().NotBeNull();
      item.Language.Name.Should().Be("en");
      item.Version.Number.Should().Be(1);
    }

    [Fact]
    public void ShouldCreateSpecificItem()
    {
      var id = ID.NewID;
      var templateId = ID.NewID;
      var database = Database.GetDatabase("master");
      var language = Language.Parse("uk-UA");
      var version = new Version(2);

      // arrange
      var item = ItemHelper.CreateInstance(database, "Home", id, templateId, ID.NewID, new FieldList(), language, version);

      // assert
      item.Name.Should().Be("Home");
      item.ID.Should().Be(id);
      item.TemplateID.Should().Be(templateId);
      item.Database.Should().Be(database);
      item.Language.Should().Be(language);
      item.Version.Should().Be(version);
    }
  }
}