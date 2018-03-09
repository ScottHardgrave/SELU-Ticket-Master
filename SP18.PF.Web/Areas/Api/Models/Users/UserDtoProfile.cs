using AutoMapper;
using SP18.PF.Core.Features.Users;

namespace SP18.PF.Web.Areas.Api.Models.Users
{
    public class UserDtoProfile : Profile
    {
        public UserDtoProfile()
        {
            CreateMap<User, UserDto>();
        }
    }
}