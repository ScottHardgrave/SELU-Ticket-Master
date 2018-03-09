using Microsoft.AspNetCore.Mvc;
using SP18.PF.Web.Models.Users;

namespace SP18.PF.Web.Areas.Admin.Models.Users
{
    public class UserCreateViewModel : UserBaseViewModel, IRegisterUser
    {
        [Remote("VerifyEmail", "Users")]
        public string Email { get; set; }
    }
}