namespace SchoolManagmentSystem.Data.AppMetaData
{
    public static class RouterParams
    {
        public const string SingleIdRoute = "/{id}";
        public const string Root = "Api";
        public const string Version = "V1";
        public const string Rule = $"{Root}/{Version}";
        public static class StudentRouting
        {
            public const string Prefix = $"{Rule}/Student";
            public const string list = $"{Prefix}/list";
            public const string paginatedList = $"{Prefix}/paginatedList";
            public const string GetById = Prefix + SingleIdRoute;
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + SingleIdRoute;
            public const string Delete = Prefix + SingleIdRoute;

        }

        public static class DepartmentRouting
        {
            public const string Prefix = $"{Rule}/Department";
            public const string list = $"{Prefix}/list";
            public const string paginatedList = $"{Prefix}/paginatedList";
            public const string GetById = Prefix + SingleIdRoute;
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + SingleIdRoute;
            public const string Delete = Prefix + SingleIdRoute;

        }

        public static class UserRouting
        {
            public const string Prefix = $"{Rule}/User";
            public const string list = $"{Prefix}/list";
            public const string paginatedList = $"{Prefix}/paginatedList";
            public const string GetById = Prefix + SingleIdRoute;
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + SingleIdRoute;
            public const string Delete = Prefix + SingleIdRoute;
            public const string ChangePassword = $"{Prefix}/ChangePassword";

        }

        public static class AuthenticationRouting
        {
            public const string Prefix = $"{Rule}/Authentication";
            //public const string list = $"{Prefix}/list";
            //public const string paginatedList = $"{Prefix}/paginatedList";
            //public const string GetById = Prefix + SingleIdRoute;
            public const string Login = Prefix + "Create";
            public const string Update = Prefix + SingleIdRoute;
            public const string Delete = Prefix + SingleIdRoute;
            public const string ChangePassword = $"{Prefix}/ChangePassword";
            public const string RefreshToken = $"{Prefix}/Refresh-Token";
            public const string ValidateToken = $"{Prefix}/Validate-Token";
            public const string ConfirmEmail = $"{Prefix}/ConfirmEmail";

        }

        public static class AuthorizationRouting
        {
            public const string Prefix = $"{Rule}/Authorization";
            public const string Roles = $"{Prefix}/Role";
            public const string Claims = $"{Prefix}/Claim";
            public const string CreateRole = $"{Roles}/Create";
            public const string EditRole = $"{Roles}/Edit";
            public const string DeleteRole = $"{Roles}/Delete" + SingleIdRoute;
            public const string GetRoles = $"{Roles}/GetRoles";
            public const string GetRoleById = $"{Roles}/GetRoleById" + SingleIdRoute;
            public const string GetUserRoles = $"{Roles}/GetUserRoles" + SingleIdRoute;
            public const string UpdateUserRoles = $"{Roles}/UpdateUserRoles";
            public const string GetUserClaims = $"{Claims}/GetUserClaims" + SingleIdRoute;
            public const string UpdateUserClaims = $"{Claims}/UpdateUserClaims";
            //public const string DeleteClaim = $"{Claims}/Delete" + SingleIdRoute;
            //public const string GetClaimById = $"{Claims}/GetClaimById" + SingleIdRoute;
            //public const string CreateClaim = $"{Claims}/Create";
            //public const string UpdateClaim = $"{Claims}/Update" + SingleIdRoute;
            //public const string ListClaims = $"{Claims}/list";
            //public const string PaginatedListClaims = $"{Claims}/paginatedList";
        }
        public static class EmailRouting
        {
            public const string Prefix = $"{Rule}/Email";
            public const string SendEmail = $"{Prefix}/SendEmail";
            public const string SendEmailWithAttachment = $"{Prefix}/SendEmailWithAttachment";
            public const string SendEmailToMultipleRecipients = $"{Prefix}/SendEmailToMultipleRecipients";
        }
    }
}
