using AutoMapper;
using SP18.PF.Core.Features.Events;

namespace SP18.PF.Web.Areas.Api.Models.Events
{
    public class EventDtoProfile : Profile
    {
        public EventDtoProfile()
        {
            CreateMap<Event, EventDto>()
                .ForMember(d => d.TourName, o => o.MapFrom(s => s.Tour.Name))
                .ForMember(d => d.VenueName, o => o.MapFrom(s => s.Venue.Name));
        }
    }
}