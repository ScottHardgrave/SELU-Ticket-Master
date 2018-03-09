using AutoMapper;
using SP18.PF.Core.Features.Venues;

namespace SP18.PF.Web.Areas.Api.Models.Venues
{
    public class VenueDtoProfile : Profile
    {
        public VenueDtoProfile()
        {
            CreateMap<Venue, VenueDto>();
        }
    }
}