using AutoMapper;
using SP18.PF.Core.Features.Shared;
using SP18.PF.Core.Features.Users;

namespace SP18.PF.Web.Areas.Api.Models.Users
{
    public class UserDtoProfile : Profile
    {
        
        
        public string Email { get; set; }

        public string Password { get; set; }

        public string ConfirmPassword { get; set; }

        public Address BillingAddress { get; set; }
    
    }
}