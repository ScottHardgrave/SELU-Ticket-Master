using SP18.PF.Core.Features.Tickets;
using SP18.PF.Web.Areas.Admin.Models.Shared;

namespace SP18.PF.Web.Areas.Admin.Models.Tickets
{
    public class TicketProfile : CrudProfile<Ticket, TicketViewModel, TicketViewModel, TicketViewModel>
    {
        public TicketProfile()
        {
            CreateMap<Ticket, TicketViewModel>()
                .ForMember(d => d.TourName, o => o.MapFrom(s => s.Event.Tour.Name))
                .ForMember(d => d.VenueName, o => o.MapFrom(s => s.Event.Venue.Name))
                .ForMember(d => d.UserName, o => o.MapFrom(s => s.User.Email));
        }
    }
}