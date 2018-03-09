namespace SP18.PF.Core.Features.Users
{
    public static class Roles
    {
        public static string[] List => new[] {Admin, Customer};

        public const string Admin = "Admin";

        public const string Customer = "Customer";
    }
}