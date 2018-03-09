using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SP18.PF.Core.Features.Events;
using SP18.PF.Core.Features.Roles;
using SP18.PF.Core.Features.Tickets;
using SP18.PF.Core.Features.Users;
using SP18.PF.Web.Areas.Admin.Models.Tickets;
using SP18.PF.Web.Extensions;
using SP18.PF.Web.Services;

namespace SP18.PF.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleNames.Admin)]
    public class TicketsController : CrudController<Ticket, TicketViewModel, TicketViewModel, TicketViewModel>
    {
        private readonly DbContext dbContext;

        public TicketsController(CrudService<Ticket, TicketViewModel, TicketViewModel, TicketViewModel> crudService, DbContext dbContext) : base(crudService)
        {
            this.dbContext = dbContext;
        }

        protected override Expression<Func<Ticket, bool>> GetSelectSingle(string id)
        {
            var item = id.SafeParseInt();
            return x => x.Id == item;
        }

        protected override async Task<TicketViewModel> GetCreateViewModel(TicketViewModel vm)
        {
            var result = vm ?? new TicketViewModel();
            var users = await dbContext.Set<User>().Select(x => new { x.Id, x.Email }).ToArrayAsync();
            result.UserSelectList = new SelectList(users, nameof(Core.Features.Users.User.Id), nameof(Core.Features.Users.User.Email));
            var events = await dbContext.Set<Event>().Select(x => new
            {
                Id = x.VenueId + "-" + x.TourId,
                Name = x.Tour.Name + " touring at " + x.Venue.Name + " for $" + x.TicketPrice
            }).ToArrayAsync();
            result.EventSelectList = new SelectList(events, "Id", "Name");
            return result;
        }

        protected override Task<TicketViewModel> GetEditViewModel(TicketViewModel vm)
        {
            return GetCreateViewModel(vm);
        }
    }
}