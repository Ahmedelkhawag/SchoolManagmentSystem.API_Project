namespace SchoolManagmentSystem.Data.Helpers
{
    public class ResetPasswordParams
    {
        public string Email { get; set; }
        public string Token { get; set; }
        public string NewPassword { get; set; }
        // public string ConfirmPassword { get; set; }

    }
}
