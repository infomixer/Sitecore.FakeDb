﻿namespace Sitecore.FakeDb.Tests
{
  using System.Linq;
  using FluentAssertions;
  using Xunit;

  public class DbItemTest
  {
    [Fact]
    public void ShouldGenerateNewIdsIfNotSet()
    {
      // arrange
      var item = new DbItem("my item");

      // act & assert
      item.ID.Should().NotBeNull();
      item.TemplateID.Should().NotBeNull();
    }

    [Fact]
    public void ShouldCreateNewDbItem()
    {
      // arrange & act
      var item = new DbItem("home");

      // assert
      item.Children.Should().BeEmpty();
      item.Fields.Should().BeEmpty();
      item.FullPath.Should().BeNull();
      item.ParentID.Should().BeNull();
    }

    [Fact]
    public void ShouldAddFields()
    {
      // arrange 
      var item = new DbItem("home") { { "Title", "Welcome!" } };

      // act & assert
      item.Fields.Should().ContainSingle(f => f.Name == "Title" && f.Value == "Welcome!");
    }

    [Fact]
    public void ShouldAddChildItem()
    {
      // arrange
      var parent = new DbItem("parent");
      var child = new DbItem("child");

      // act
      parent.Add(child);

      // assert
      parent.Children.Single().Should().BeEquivalentTo(child);
    }
  }
}