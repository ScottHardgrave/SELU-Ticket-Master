namespace SP18.PF.Web.Areas.Admin.Models.Users
{
    public class LoginViewModel
    {
        public string ReturnUrl { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RemeberMe { get; set; }
    }
}