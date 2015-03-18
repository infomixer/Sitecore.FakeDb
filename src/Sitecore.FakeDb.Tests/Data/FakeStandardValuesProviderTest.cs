namespace Sitecore.FakeDb.Tests.Data
{
  using FluentAssertions;
  using Sitecore.Data;
  using Sitecore.Data.Fields;
  using Sitecore.FakeDb.Data;
  using Sitecore.FakeDb.Data.Engines;
  using Sitecore.FakeDb.Data.Items;
  using Xunit;

  public class FakeStandardValuesProviderTest
  {
    private readonly FakeStandardValuesProvider provider;

    public FakeStandardValuesProviderTest()
    {
      this.provider = new FakeStandardValuesProvider();
    }

    [Fact]
    public void ShouldRequireDataStorage()
    {
      // act & assert
      this.provider.Should().BeAssignableTo<IRequireDataStorage>();
    }

    [Fact]
    public void ShouldSetDataStorage()
    {
      // arrange
      var storage = new DataStorage();

      // act
      ((IRequireDataStorage)this.provider).SetDataStorage(storage);

      // assert
      ((IRequireDataStorage)this.provider).DataStorage.Should().BeSameAs(storage);
    }

    [Fact]
    public void ShouldReturnEmptyStringIfNoTemplateFound()
    {
      // arrange
      var storage = new DataStorage();
      ((IRequireDataStorage)this.provider).SetDataStorage(storage);

      var field = new Field(ID.NewID, ItemHelper.CreateInstance());

      // act & assert
      this.provider.GetStandardValue(field).Should().BeEmpty();
    }
  }
}