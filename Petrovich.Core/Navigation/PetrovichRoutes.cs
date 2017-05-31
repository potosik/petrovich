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

        public static class DataStructure
        {
            private const string Controller = "DataStructure";

            public static Endpoint CategoryList = new Endpoint(Controller, "CategoryList");
            public static Endpoint CategoryCreate = new Endpoint(Controller, "CategoryCreate");
            public static Endpoint CategoryEdit = new Endpoint(Controller, "CategoryEdit");
            public static Endpoint CategoryDelete = new Endpoint(Controller, "CategoryDelete");

            public static Endpoint SubcategoryList = new Endpoint(Controller, "SubcategoryList");
            public static Endpoint SubcategoryCreate = new Endpoint(Controller, "SubcategoryCreate");
            public static Endpoint SubcategoryEdit = new Endpoint(Controller, "SubcategoryEdit");
            public static Endpoint SubcategoryDelete = new Endpoint(Controller, "SubcategoryDelete");

            public static Endpoint GroupList = new Endpoint(Controller, "GroupList");
            public static Endpoint GroupCreate = new Endpoint(Controller, "GroupCreate");
            public static Endpoint GroupEdit = new Endpoint(Controller, "GroupEdit");
            public static Endpoint GroupDelete = new Endpoint(Controller, "GroupDelete");
        }

        public static class Error
        {
            private const string Controller = "Error";

            public static Endpoint Index = new Endpoint(Controller, IndexAction);
            public static Endpoint NotFound = new Endpoint(Controller, "NotFound");
            public static Endpoint BadRequest = new Endpoint(Controller, "BadRequest");
        }

        public static class Products
        {
            private const string Controller = "Products";

            public static Endpoint Index = new Endpoint(Controller, IndexAction);
            public static Endpoint Create = new Endpoint(Controller, "Create");
            public static Endpoint Edit = new Endpoint(Controller, "Edit");
            public static Endpoint Delete = new Endpoint(Controller, "Delete");
        }

        public static class UserManagement
        {
            private const string Controller = "UserManagement";

            public static Endpoint Active = new Endpoint(Controller, "Active");
            public static Endpoint Deleted = new Endpoint(Controller, "Deleted");
            public static Endpoint Create = new Endpoint(Controller, "Create");
            public static Endpoint Edit = new Endpoint(Controller, "Edit");
            public static Endpoint Delete = new Endpoint(Controller, "Delete");
            public static Endpoint Restore = new Endpoint(Controller, "Restore");
        }
    }
}
