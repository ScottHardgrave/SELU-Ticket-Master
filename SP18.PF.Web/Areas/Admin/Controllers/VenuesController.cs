using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP18.PF.Core.Features.Roles;
using SP18.PF.Core.Features.Venues;
using SP18.PF.Web.Areas.Admin.Models.Venues;
using SP18.PF.Web.Extensions;
using SP18.PF.Web.Services;

namespace SP18.PF.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleNames.Admin)]
    public class VenuesController : CrudController<Venue, VenueViewModel, VenueViewModel, VenueViewModel>
    {
        public VenuesController(CrudService<Venue, VenueViewModel, VenueViewModel, VenueViewModel> crudService) : base(crudService)
        {
        }

        protected override Task<VenueViewModel> GetCreateViewModel(VenueViewModel vm)
        {
            return Task.FromResult(vm ?? new VenueViewModel());
        }

        protected override Expression<Func<Venue, bool>> GetSelectSingle(string id)
        {
            var item = id.SafeParseInt();
            return x => x.Id == item;
        }
    }
}