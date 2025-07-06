namespace SchoolManagmentSystem.Data.Results
{
    public class ManageUserRolesResult
    {

        public int userId { get; set; }
        public List<UserRoles> userRoles { get; set; }
    }

    public class UserRoles
    {
        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public bool IsSelected { get; set; }
    }
}
