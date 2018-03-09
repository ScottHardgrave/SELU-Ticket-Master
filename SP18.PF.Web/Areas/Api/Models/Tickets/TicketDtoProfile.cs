using AutoMapper;
using SP18.PF.Core.Features.Tickets;

namespace SP18.PF.Web.Areas.Api.Models.Tickets
{
    public class TicketDtoProfile : Profile
    {
        public TicketDtoProfile()
        {
            CreateMap<Ticket, TicketDto>();
        }
    }
}