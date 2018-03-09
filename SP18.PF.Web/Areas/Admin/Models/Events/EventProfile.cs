using SP18.PF.Core.Features.Events;
using SP18.PF.Web.Areas.Admin.Models.Shared;

namespace SP18.PF.Web.Areas.Admin.Models.Events
{
    public class EventProfile : CrudProfile<Event, EventListingViewModel, EventCreateViewModel, EventEditViewModel>
    {
        public EventProfile()
        {
            CreateMap<Event, EventBaseViewModel>()
                .ForMember(d => d.TourName, o => o.MapFrom(s => s.Tour.Name))
                .ForMember(d => d.VenueName, o => o.MapFrom(s => s.Venue.Name));
            CreateMap<EventEditViewModel, Event>()
                .ForMember(d => d.TourId, o => o.Ignore())
                .ForMember(d => d.VenueId, o => o.Ignore());
        }
    }
}