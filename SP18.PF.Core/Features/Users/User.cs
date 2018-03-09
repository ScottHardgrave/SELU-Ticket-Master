using SP18.PF.Core.Features.Shared;

namespace SP18.PF.Core.Features.Users
{
    public class User
    {
        public int Id { get; set; }

        public string Email { get; set; }

        public Address BillingAddress { get; set; }

        public string Password { get; set; }

        public string Role { get; set; }
    }
}