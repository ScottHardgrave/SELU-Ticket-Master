using System;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SP18.PF.Core.Features.Roles;
using SP18.PF.Core.Features.Tours;
using SP18.PF.Web.Areas.Admin.Models.Tours;
using SP18.PF.Web.Extensions;
using SP18.PF.Web.Services;

namespace SP18.PF.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = RoleNames.Admin)]
    public class ToursController : CrudController<Tour, TourViewModel, TourViewModel, TourViewModel>
    {
        public ToursController(CrudService<Tour, TourViewModel, TourViewModel, TourViewModel> crudService) : base(crudService)
        {
        }

        protected override Expression<Func<Tour, bool>> GetSelectSingle(string id)
        {
            var item = id.SafeParseInt();
            return x => x.Id == item;
        }
    }
}