namespace Sitecore.FakeDb.Tests
{
  using FluentAssertions;
  using NSubstitute;
  using Sitecore.Data;
  using Sitecore.Links;
  using Sitecore.Security.Accounts;
  using Sitecore.Security.Authentication;
  using System.Web.Security;
  using Xunit;

  public class DbProviderSwitcherTest
  {
    [Fact]
    public void ShouldSwitchAndDisposeAuthenticationProvider()
    {
      // arrange
      var provider = Substitute.For<AuthenticationProvider>();
      var user = User.FromName("Rambo John", true);
      provider.GetActiveUser().Returns(user);

      using (var db = new Db())
      {
        // act
        db.Providers.Switch<AuthenticationProvider>(provider);

        // assert
        AuthenticationManager.GetActiveUser().Should().BeSameAs(user);
      }

      AuthenticationManager.GetActiveUser().Should().NotBeSameAs(user);
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
      var roleProvider = Substitute.For<RoleProvider>();
      var rolesInRolesProvider = Substitute.For<RolesInRolesProvider>();

      using (var db = new Db())
      {
        // act
        db.Providers
          .Switch<RoleProvider>(roleProvider)
          .Switch<RolesInRolesProvider>(rolesInRolesProvider);

        Roles.GetAllRoles();
        RolesInRolesManager.GetAllRoles(true);

        // assert        
        roleProvider.Received().GetAllRoles();
        rolesInRolesProvider.Received().GetAllRoles(true);
      }
    }
  }
}