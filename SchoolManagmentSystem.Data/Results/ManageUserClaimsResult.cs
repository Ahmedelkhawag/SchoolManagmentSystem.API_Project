namespace SchoolManagmentSystem.Data.Results
{
    public class ManageUserClaimsResult
    {
        public int userId { get; set; }
        public List<UserClaim> userClaims { get; set; }
    }

    public class UserClaim
    {
        public string ClaimType { get; set; }
        public string ClaimValue { get; set; }
        public bool IsSelected { get; set; }
    }
}
