using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;
using SP18.PF.Core.Features.Shared;
using SP18.PF.Core.Features.Users;
using SP18.PF.Web.Helpers;

namespace SP18.PF.Web.Areas.Admin.Models.Users
{
    public abstract class UserBaseViewModel
    {
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        public Address BillingAddress { get; set; }

        public string Role { get; set; }

        public string HashedPassword => CryptoHelpers.HashPassword(Password);

        public SelectList StateList => new SelectList(States.List, nameof(State.Abbr), nameof(State.Name));

        public SelectList RoleList => new SelectList(Roles.List, Role);
    }
}