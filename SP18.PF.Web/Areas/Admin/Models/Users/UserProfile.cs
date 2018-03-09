using SP18.PF.Core.Features.Users;
using SP18.PF.Web.Areas.Admin.Models.Shared;

namespace SP18.PF.Web.Areas.Admin.Models.Users
{
    public class UserProfile : CrudProfile<User, UserListViewModel, UserCreateViewModel, UserEditViewModel>
    {
        public UserProfile()
        {
            CreateMap<UserCreateViewModel, User>()
                .ForMember(d => d.Password, o => o.MapFrom(s => s.HashedPassword));
            CreateMap<UserEditViewModel, User>()
                .ForMember(d => d.Password, o =>
                {
                    o.Condition(s => !string.IsNullOrWhiteSpace(s.Password));
                    o.MapFrom(s => s.HashedPassword);
                })
                .ForMember(d => d.Email, o => o.Ignore())
                .ForMember(d => d.Id, o => o.Ignore());
            CreateMap<User, UserEditViewModel>()
                .ForMember(d => d.Password, o => o.Ignore());
        }
    }
}