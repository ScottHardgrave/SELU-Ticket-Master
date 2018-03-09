using System;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using SP18.PF.Core.Features.Events;
using SP18.PF.Core.Features.Tickets;
using SP18.PF.Core.Features.Users;
using SP18.PF.Web.Areas.Api.Models.Tickets;

namespace SP18.PF.Web.Services
{
    public class TicketService
    {
        private readonly DbContext context;
        private readonly IMapper mapper;

        public TicketService(DbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }
        public Task<TicketDto[]> GetUserTickets(ClaimsPrincipal user, Expression<Func<Ticket, bool>> filter = null)
        {
            filter = filter ?? (x => true);
            var userEmail = user?.Identity?.Name;
            var tickets = context.Set<Ticket>()
                .Where(x => x.User.Email == userEmail)
                .Where(filter)
                .ProjectTo<TicketDto>(mapper.ConfigurationProvider)
                .ToArrayAsync();
            return tickets;
        }

        public async Task<ServiceResponse<TicketDto>> Purchase(ClaimsPrincipal user, int eventId)
        {
            var userEmail = user?.Identity?.Name;
            var userId = await context.Set<User>()
                .Where(x => x.Email == userEmail)
                .Select(x => (int?) x.Id)
                .SingleOrDefaultAsync();
            if (userId == null)
            {
                return new ServiceResponse<TicketDto>
                {
                    Errors = {{"user", new[] {"No such user"}}}
                };
            }
            var eventRecord = await context.Set<Event>()
                .Where(x => x.Id == eventId)
                .Select(x => new {x.Id, x.Venue.Capacity, x.TicketPrice})
                .SingleOrDefaultAsync();
            if (eventRecord == null)
            {
                return new ServiceResponse<TicketDto>
                {
                    Errors = { { "eventId", new[] { "No such event" } } }
                };
            }

            var tickets = await context.Set<Ticket>().CountAsync(x => x.EventId == eventId);
            if (tickets >= eventRecord.Capacity)
            {
                return new ServiceResponse<TicketDto>
                {
                    Errors = { { "eventId", new[] { "Event is sold out" } } }
                };
            }
            var ticket = new Ticket
            {
                EventId = eventRecord.Id,
                UserId = userId.Value,
                PurchasePrice = eventRecord.TicketPrice
            };
            var addedRecord = context.Add(ticket);
            context.SaveChanges();

            var resultDto = await context.Set<Ticket>()
                .Where(x=>x.Id == addedRecord.Entity.Id)
                .ProjectTo<TicketDto>(mapper.ConfigurationProvider)
                .SingleAsync();
            return new ServiceResponse<TicketDto>
            {
                Data = resultDto
            };
        }
    }
}