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
            public const string GetById = Prefix + SingleIdRoute;
            public const string Create = Prefix + "Create";
            public const string Update = Prefix + SingleIdRoute;

        }
    }
}
