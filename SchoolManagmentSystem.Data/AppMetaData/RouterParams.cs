﻿namespace SchoolManagmentSystem.Data.AppMetaData
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

        }

        public static class AuthorizationRouting
        {
            public const string Prefix = $"{Rule}/Authorization";
            public const string CreateRole = $"{Prefix}/Role/Create";
            public const string EditRole = $"{Prefix}/Role/Edit";
            public const string DeleteRole = $"{Prefix}/Role/Delete" + SingleIdRoute;
            public const string GetRoles = $"{Prefix}/Role/GetRoles";
            public const string GetRoleById = $"{Prefix}/Role/GetRoleById" + SingleIdRoute;
            public const string GetUserRoles = $"{Prefix}/Role/GetUserRoles" + SingleIdRoute;
        }
    }
}
