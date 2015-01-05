namespace Sitecore.FakeDb.Tests.Pipelines.InitFakeDb
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Data;
  using Sitecore.Data.IDTables;
  using Sitecore.Data.Proxies;
  using Sitecore.FakeDb.Data.Engines;
  using Sitecore.FakeDb.Data.IDTables;
  using Sitecore.FakeDb.Links;
  using Sitecore.FakeDb.Pipelines.InitFakeDb;
  using Sitecore.FakeDb.Resources.Media;
  using Sitecore.FakeDb.Security.AccessControl;
  using Sitecore.FakeDb.Security.Accounts;
  using Sitecore.FakeDb.Security.Web;
  using Sitecore.FakeDb.Tasks;
  using Sitecore.Globalization;
  using Sitecore.Links;
  using Sitecore.Resources.Media;
  using Sitecore.Security;
  using Sitecore.Security.AccessControl;
  using Sitecore.Security.Accounts;
  using Sitecore.Security.Authentication;
  using Sitecore.Security.Domains;
  using Sitecore.SecurityModel;
  using Sitecore.Sites;
  using Sitecore.Tasks;
  using Sitecore.Workflows;
  using System;
  using System.Globalization;
  using System.Web.Profile;
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
    public void ShouldRegisterThreadLocalProviderSwitchers(Type providerType, Type switcherType)
    {
      // arrange
      var providers = Substitute.For<DbProviderSet>(Substitute.For<IProviderSwitcherFactory>());
      var args = new InitDbArgs(database, dataStorage) { Providers = providers };

      // act
      processor.Process(args);

      // assert
      providers.Received().RegisterSwitcher(providerType, switcherType);
    }

    [Theory]
    [InlineData(typeof(AuthenticationProvider), typeof(AuthenticationSwitcher))]
    [InlineData(typeof(CultureInfo), typeof(ThreadCultureSwitcher))]
    [InlineData(typeof(AuthorizationProvider), typeof(Sitecore.Security.AccessControl.AuthorizationSwitcher))]
    [InlineData(typeof(Domain), typeof(DomainSwitcher))]
    [InlineData(typeof(ProfileProvider), typeof(ProfileSwitcher))]
    [InlineData(typeof(SecurityState), typeof(SecurityStateSwitcher))]
    [InlineData(typeof(User), typeof(UserSwitcher))]
    [InlineData(typeof(WorkflowContextState), typeof(WorkflowContextStateSwitcher))]
    //[InlineData(typeof(SiteContext), typeof(SiteContextSwitcher))]
    public void ShouldRegisterCommonSitecoreSwitchers(Type providerType, Type switcherType)
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