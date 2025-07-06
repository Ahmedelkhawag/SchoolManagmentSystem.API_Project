using System.Security.Claims;

namespace SchoolManagmentSystem.Data.Helpers
{
    public static class ClaimsStore
    {
        public const string PermessionClaimType = "Permissions";
        public static List<Claim> Claims = new List<Claim>
        {
            new Claim(PermessionClaimType, "CanViewDashboard"),
            new Claim(PermessionClaimType, "CanEditDashboard"),
            new Claim(PermessionClaimType, "CanViewUsers"),
            new Claim(PermessionClaimType, "CanEditUsers"),
            new Claim(PermessionClaimType, "CanDeleteUsers"),
            new Claim(PermessionClaimType, "CanCreateUsers"),
            new Claim(PermessionClaimType, "CanViewRoles"),
            new Claim(PermessionClaimType, "CanEditRoles"),
            new Claim(PermessionClaimType, "CanDeleteRoles"),
            new Claim(PermessionClaimType, "CanCreateRoles"),
        };

    }
}
