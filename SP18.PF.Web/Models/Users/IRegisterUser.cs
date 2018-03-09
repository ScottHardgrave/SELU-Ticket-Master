using SP18.PF.Core.Features.Shared;

namespace SP18.PF.Web.Models.Users
{
    public interface IRegisterUser
    {
        string Email { get; set; }

        string Password { get; set; }

        string ConfirmPassword { get; set; }

        Address BillingAddress { get; set; }
    }
}