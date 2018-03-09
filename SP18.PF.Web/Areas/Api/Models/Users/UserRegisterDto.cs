using SP18.PF.Core.Features.Shared;
using SP18.PF.Web.Models.Users;

namespace SP18.PF.Web.Areas.Api.Models.Users
{
    public class UserRegisterDto : IRegisterUser
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public Address BillingAddress { get; set; }
    }
}