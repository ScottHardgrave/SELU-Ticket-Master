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
using SP18.PF.Core.Features.Tours;
using SP18.PF.Core.Features.Venues;
using SP18.PF.Web.Areas.Admin.Models.Events;
using SP18.PF.Web.Extensions;
using SP18.PF.Web.Services;

namespace SP18.PF.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleNames.Admin)]
    public class EventsController : CrudController<Event, EventListingViewModel, EventCreateViewModel, EventEditViewModel>
    {
        private readonly DbContext dbContext;

        public EventsController(CrudService<Event, EventListingViewModel, EventCreateViewModel, EventEditViewModel> crudService, DbContext dbContext) : base(crudService)
        {
            this.dbContext = dbContext;
        }
        
        protected override Expression<Func<Event, bool>> GetSelectSingle(string id)
        {
            var item = id.SafeParseInt();
            return x => x.Id == item;
        }

        protected override async Task<EventCreateViewModel> GetCreateViewModel(EventCreateViewModel vm)
        {
            var result = vm ?? new EventCreateViewModel();
            var tours = await dbContext.Set<Tour>().Select(x => new {x.Id, x.Name}).ToArrayAsync();
            result.TourSelectList = new SelectList(tours, nameof(Tour.Id), nameof(Tour.Name));
            var venues = await dbContext.Set<Venue>().Select(x => new { x.Id, x.Name }).ToArrayAsync();
            result.VenueSelectList = new SelectList(venues, nameof(Venue.Id), nameof(Venue.Name));
            return result;
        }
    }
}