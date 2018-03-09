using AutoMapper;
using SP18.PF.Core.Features.Tours;

namespace SP18.PF.Web.Areas.Api.Models.Tours
{
    public class TourDtoProfile : Profile
    {
        public TourDtoProfile()
        {
            CreateMap<Tour, TourDto>();
        }
    }
}