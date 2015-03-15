namespace Sitecore.FakeDb.Tests.Pipelines.AddDbItem
{
  using System;
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Data;
  using Sitecore.FakeDb.Data.Engines;
  using Sitecore.FakeDb.Pipelines.AddDbItem;
  using Xunit;

  public class EnsureIsChildTest
  {
    [Fact]
    public void ShouldThrowIfNoParentFound()
    {
      // arrange
      var item = new DbItem("some child", @ID.Parse("{472CABE4-9483-49AC-89F7-8AC9D770DD82}"));

      var dataStorage = Substitute.For<DataStorage>(Database.GetDatabase("master"));

      var processor = new EnsureIsChild();
      var args = new AddDbItemArgs(item, dataStorage);

      // act
      Action action = () => processor.Process(args);

      // assert
      action.ShouldThrow<InvalidOperationException>().WithMessage(@"Parent not found for item ""some child"", ""{472CABE4-9483-49AC-89F7-8AC9D770DD82}""");
    }
  }
}