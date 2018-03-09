namespace SP18.PF.Web.Areas.Api.Models.Users
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RemeberMe { get; set; }
    }
}