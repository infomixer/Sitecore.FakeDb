namespace Sitecore.FakeDb.Tests.Pipelines.InitFakeDb
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Data;
  using Sitecore.Data.IDTables;
  using Sitecore.FakeDb.Data.Engines;
  using Sitecore.FakeDb.Data.IDTables;
  using Sitecore.FakeDb.Links;
  using Sitecore.FakeDb.Pipelines.InitFakeDb;
  using Sitecore.FakeDb.Resources.Media;
  using Sitecore.FakeDb.Security.AccessControl;
  using Sitecore.FakeDb.Security.Accounts;
  using Sitecore.FakeDb.Security.Web;
  using Sitecore.FakeDb.Tasks;
  using Sitecore.Links;
  using Sitecore.Resources.Media;
  using Sitecore.Security.AccessControl;
  using Sitecore.Security.Accounts;
  using Sitecore.Tasks;
  using System;
  using System.Web.Security;
  using Xunit;
  using Xunit.Extensions;

  public class RegisterDbProviderSwitchersTest
  {
    private readonly Database database;

    private readonly DataStorage dataStorage;

    private readonly RegisterDbProviderSwitchers processor;

    public RegisterDbProviderSwitchersTest()
    {
      this.database = Database.GetDatabase("master");
      this.dataStorage = Substitute.For<DataStorage>(this.database);

      this.processor = new RegisterDbProviderSwitchers();
    }

    [Fact]
    public void ShouldDoNothingIfNoDbProviderSetExists()
    {
      // arrange
      var args = new InitDbArgs(database, dataStorage) { Providers = null };

      // act
      Assert.DoesNotThrow(() => this.processor.Process(args));
    }

    [Theory]
    [InlineData(typeof(IDTableProvider), typeof(IDTableProviderSwitcher))]
    [InlineData(typeof(LinkDatabase), typeof(LinkDatabaseSwitcher))]
    [InlineData(typeof(AuthorizationProvider), typeof(Sitecore.FakeDb.Security.AccessControl.AuthorizationSwitcher))]
    [InlineData(typeof(RolesInRolesProvider), typeof(RolesInRolesSwitcher))]
    [InlineData(typeof(MembershipProvider), typeof(MembershipSwitcher))]
    [InlineData(typeof(MediaProvider), typeof(MediaProviderSwitcher))]
    [InlineData(typeof(RoleProvider), typeof(RoleProviderSwitcher))]
    [InlineData(typeof(TaskDatabase), typeof(TaskDatabaseSwitcher))]
    public void ShouldRegisterDbProviderSwitchers(Type providerType, Type switcherType)
    {
      // arrange
      var providers = Substitute.For<DbProviderSet>(Substitute.For<IProviderSwitcherFactory>());
      var args = new InitDbArgs(database, dataStorage) { Providers = providers };

      // act
      processor.Process(args);

      // assert
      providers.Received().RegisterSwitcher(providerType, switcherType);
    }
  }
}
