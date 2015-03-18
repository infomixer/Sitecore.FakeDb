namespace Sitecore.FakeDb.Tests.Data.Engines.DataCommands
{
  using NSubstitute;
  using Sitecore.Data;
  using Sitecore.FakeDb.Data.Engines;
  using Sitecore.FakeDb.Data.Engines.DataCommands;

  public abstract class CommandTestBase
  {
    protected readonly Database database;

    protected readonly DataStorage dataStorage;

    protected readonly DataEngineCommand innerCommand;

    protected CommandTestBase()
    {
      this.database = Database.GetDatabase("master");
      this.dataStorage = Substitute.For<DataStorage>(this.database);

      this.innerCommand = Substitute.For<DataEngineCommand>();
      this.innerCommand.DataStorage.Returns(this.dataStorage);
    }
  }
}