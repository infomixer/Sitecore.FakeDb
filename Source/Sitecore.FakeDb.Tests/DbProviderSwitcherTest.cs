namespace Sitecore.FakeDb.Tests
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Data;
  using Sitecore.Links;
  using Sitecore.Security.Accounts;
  using System.Web.Security;
  using Xunit;

  public class DbProviderSwitcherTest
  {
    [Fact]
    public void ShouldSwitchAndDisposeLinkDatabase()
    {
      // arrange
      var provider = Substitute.For<LinkDatabase>();
      var database = Database.GetDatabase("master");
      var brokenLinks = new ItemLink[] { };

      provider.GetBrokenLinks(database).Returns(brokenLinks);

      using (var db = new Db())
      {
        // act
        db.Providers.Switch<LinkDatabase>(provider);

        // assert
        Globals.LinkDatabase.GetBrokenLinks(database).Should().BeSameAs(brokenLinks);
      }

      Globals.LinkDatabase.GetBrokenLinks(database).Should().BeEmpty();
    }

    [Fact]
    public void ShouldSwitchAndDisposeRoleProvider()
    {
      // arrange
      var provider = Substitute.For<RoleProvider>();
      var roles = new[] { "sitecore\\Developers" };
      provider.GetAllRoles().Returns(roles);

      using (var db = new Db())
      {
        // act
        db.Providers.Switch<RoleProvider>(provider);

        // assert
        Roles.GetAllRoles().Should().BeSameAs(roles);
      }

      Roles.GetAllRoles().Should().BeEmpty();
    }

    [Fact]
    public void ShouldSwitchProvidersUsingFluentApi()
    {
      // arrange
      var linkDatabase = Substitute.For<LinkDatabase>();
      var roleProvider = Substitute.For<RoleProvider>();
      var rolesInRolesProvider = Substitute.For<RolesInRolesProvider>();

      using (var db = new Db())
      {
        // act
        db.Providers
          .Switch<LinkDatabase>(linkDatabase)
          .Switch<RoleProvider>(roleProvider)
          .Switch<RolesInRolesProvider>(rolesInRolesProvider);

        Globals.LinkDatabase.GetBrokenLinks(db.Database);
        Roles.GetAllRoles();
        RolesInRolesManager.GetAllRoles(true);

        // assert        
        linkDatabase.Received().GetBrokenLinks(db.Database);
        roleProvider.Received().GetAllRoles();
        rolesInRolesProvider.Received().GetAllRoles(true);
      }
    }
  }
}