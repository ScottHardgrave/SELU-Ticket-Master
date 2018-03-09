using AutoMapper;
using SP18.PF.Core.Features.Shared;

namespace SP18.PF.Web.Areas.Admin.Models.Shared
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<Address, Address>();
        }
    }
}