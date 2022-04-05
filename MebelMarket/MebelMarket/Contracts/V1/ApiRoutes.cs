namespace MebelMarket.Contracts.V1
{
    internal static class ApiRoutes
    {
        internal const string Base = "api/v1/[controller]/";

        internal static class Shared
        {
            internal const string Get = "{id}";
            internal const string GetAll = "All";
            internal const string Create = "Create";
            internal const string Update = "Update";
            internal const string Delete = "Delete/{id}";
        }

        internal static class Account
        {
            internal const string Register = "Register";
            internal const string Login = "Login";
        }
    }
}
