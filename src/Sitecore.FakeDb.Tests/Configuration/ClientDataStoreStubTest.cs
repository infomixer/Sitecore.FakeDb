namespace Sitecore.FakeDb.Tests.Configuration
{
  using System;
  using FluentAssertions;
  using Sitecore.FakeDb.Configuration;
  using Xunit;

  public class ClientDataStoreStubTest
  {
    private readonly OpenClientDataStoreStub dataStore;

    public ClientDataStoreStubTest()
    {
      this.dataStore = new OpenClientDataStoreStub();
    }

    [Fact]
    public void ShouldNotThrowExceptionOnCompactData()
    {
      // act
      Action action = () => this.dataStore.CompactData();

      // asset
      action.ShouldNotThrow();
    }

    [Fact]
    public void ShouldReturnNullOnLoadData()
    {
      // act & assert
      this.dataStore.LoadData("key").Should().BeNull();
    }

    [Fact]
    public void ShouldNotThrowExceptionOnSaveData()
    {
      // act
      Action action = () => this.dataStore.SaveData("key", "data");

      // asset
      action.ShouldNotThrow();
    }

    [Fact]
    public void ShouldNotThrowExceptionOnRemoveData()
    {
      // act
      Action action = () => this.dataStore.RemoveData("key");

      // asset
      action.ShouldNotThrow();
    }

    private class OpenClientDataStoreStub : ClientDataStoreStub
    {
      new public void CompactData()
      {
        base.CompactData();
      }

      new public string LoadData(string key)
      {
        return base.LoadData(key);
      }

      new public void SaveData(string key, string data)
      {
        base.SaveData(key, data);
      }

      new public void RemoveData(string key)
      {
        base.RemoveData(key);
      }
    }
  }
}