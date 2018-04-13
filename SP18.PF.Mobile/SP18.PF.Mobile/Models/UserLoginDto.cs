using System;
using System.Collections.Generic;
using System.Text;

namespace SP18.PF.Mobile.Models
{
    public class UserLoginDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public bool RemeberMe { get; set; }
    }
}
