namespace Petrovich.Core.Navigation
{
    public static class PetrovichRoutes
    {
        private const string IndexAction = "Index";

        public static class Account
        {
            private const string Controller = "Account";

            public static Endpoint Login = new Endpoint(Controller, "Login");
            public static Endpoint Logout = new Endpoint(Controller, "Logout");
            public static Endpoint ChangePassword = new Endpoint(Controller, "ChangePassword");
            public static Endpoint ChangePasswordSuccess = new Endpoint(Controller, "ChangePasswordSuccess");
        }

        public static class Dashboard
        {
            private const string Controller = "Dashboard";

            public static Endpoint Index = new Endpoint(Controller, IndexAction);
        }

        public static class Error
        {
            private const string Controller = "Error";

            public static Endpoint Index = new Endpoint(Controller, IndexAction);
            public static Endpoint NotFound = new Endpoint(Controller, "NotFound");
        }

        public static class ManageUsers
        {
            private const string Controller = "ManageUsers";

            public static Endpoint Active = new Endpoint(Controller, "Active");
            public static Endpoint Deleted = new Endpoint(Controller, "Deleted");
        }
    }
}
