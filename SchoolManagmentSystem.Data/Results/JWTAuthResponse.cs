namespace SchoolManagmentSystem.Data.Results
{
    public class JWTAuthResponse
    {
        public string AccessToken { get; set; }
        public RefreshToken RefreshToken { get; set; }
    }

    public class RefreshToken
    {

        public string Refresh_Token { get; set; }
        public DateTime ExpireAt { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}
